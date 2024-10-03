using BookingHotel.Core.DTO;
using BackendAPIBookingHotel.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using BookingHotel.Core;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


public class AuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<string> RegisterAsync(RegisterDto model)
    {
        var person = new Person
        {
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        await _unitOfWork.Repository<Person>().AddAsync(person);
        await _unitOfWork.SaveChangesAsync();

        var email = new Email
        {
            EmailAddress = model.Email,
            PersonID = person.PersonID,
            EmailType = model.EmailType,
        };
        await _unitOfWork.Repository<Email>().AddAsync(email);
        await _unitOfWork.SaveChangesAsync();

        // Tạo salt và hash mật khẩu
        var (hashedPassword, salt) = HashPassword(model.Password);

        var user = new User
        {
            UserID = person.PersonID,
            Username = model.UserName,
            PasswordHash = hashedPassword,
            PasswordSalt = Convert.ToBase64String(salt),  // Lưu salt
            CreateDate = DateTime.UtcNow
        };
        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Thêm Role cho User
        var userRole = new UserRole
        {
            UserID = user.UserID,
            RoleID = 2 // Gán role mặc định là User
        };
        await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
        await _unitOfWork.SaveChangesAsync();

        return "User registered successfully!";
    }
    public async Task<(string AccessToken, string RefreshToken, UserDto UserInfo)> LoginAsync(LoginDto model)
    {
        var user = await _unitOfWork.Repository<User>().GetAsync(u => u.Username == model.Username);

        if (user == null)
        {
            throw new UnauthorizedAccessException("UserName Or Password is invalid!!!");
        }

        // Chuyển đổi PasswordSalt từ string sang byte[]
        var saltBytes = Convert.FromBase64String(user.PasswordSalt);

        if (!VerifyPassword(model.Password, user.PasswordHash, saltBytes))
        {
            throw new UnauthorizedAccessException("UserName Or Password is invalid!!!");
        }

        // Lấy thông tin Person tương ứng với User
        var person = await _unitOfWork.Repository<Person>().GetAsync(p => p.PersonID == user.UserID);

        var roles = await _unitOfWork.Repository<UserRole>().GetAllAsync(ur => ur.UserID == user.UserID);

        // Tạo claims cho JWT
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString() ?? string.Empty),
        new Claim(ClaimTypes.Name, user.Username)
    };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.RoleID.ToString()));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(
         issuer: _configuration["Jwt:Issuer"],
         audience: _configuration["Jwt:Audience"],
         expires: DateTime.UtcNow.AddHours(7).AddMinutes(30), // Chuyển sang giờ Việt Nam và thêm 30 phút
         signingCredentials: creds,
         claims: claims
     );

        // Generate the Refresh Token
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshToken_ExpriredTime = DateTime.UtcNow.AddHours(7).AddDays(7).ToString("yyyy-MM-dd HH:mm:ss"); // Set expiration time to 7 days later

        // Save the Refresh Token in the User table
        _unitOfWork.Repository<User>().UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
        // Tạo UserDto để trả về thông tin người dùng
        var userInfo = new UserDto
        {
            UserID = user.UserID,
            Username = user.Username,
            FirstName = person.FirstName,  // Lấy thông tin FirstName từ Person
            LastName = person.LastName      // Lấy thông tin LastName từ Person
        };

        // Trả về Access Token, Refresh Token và thông tin người dùng
        return (new JwtSecurityTokenHandler().WriteToken(accessToken), refreshToken, userInfo);
    }

    private (string hashedPassword, byte[] salt) HashPassword(string password)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return (hashed, salt);
    }

    private bool VerifyPassword(string password, string storedHash, byte[] storedSalt)
    {
        string hashedInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: storedSalt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return hashedInput == storedHash;
    }

    private string GenerateRefreshToken()
    {
        // Tạo mảng byte ngẫu nhiên có độ dài được chỉ định
        var randomNumber = new byte[32];

        // Sử dụng RandomNumberGenerator để tạo số ngẫu nhiên an toàn
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        // Chuyển mảng byte thành chuỗi base64 để dễ dàng lưu trữ
        return Convert.ToBase64String(randomNumber);
    }

}
