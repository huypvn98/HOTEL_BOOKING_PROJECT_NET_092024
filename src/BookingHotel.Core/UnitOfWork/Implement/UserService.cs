// UserService.cs

using BackendAPIBookingHotel.Model;
using BookingHotel.Core;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UserDtoNew>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync(u => u.isActive);

        if (users == null || !users.Any())
        {
            return new List<UserDtoNew>();
        }

        var userDtos = new List<UserDtoNew>();

        foreach (var u in users)
        {
            // Lấy Person tương ứng với User
            var person = await _unitOfWork.Repository<Person>().GetAsync(p => p.PersonID == u.UserID);

            // Kiểm tra person có null không
            if (person == null)
            {
                // Log lỗi hoặc xử lý nếu cần
                Console.WriteLine($"No person found for UserID: {u.UserID}");
                continue; // Bỏ qua user này
            }

            // Lấy tất cả Email liên quan đến Person
            var emails = await _unitOfWork.Repository<Email>().GetAllAsync(e => e.PersonID == person.PersonID);
            var primaryEmail = emails?.FirstOrDefault(e => e.IsPrimary)?.EmailAddress; // Lấy Email chính


            // Lấy tất cả UserRoles liên quan đến User
            var userRoles = await _unitOfWork.Repository<UserRole>().GetAllAsync(r => r.UserID == u.UserID);

            // Tạo danh sách các tên vai trò
            var roleNames = new List<string>();

            foreach (var userRole in userRoles)
            {
                // Lấy Role dựa trên RoleID
                var role = await _unitOfWork.Repository<Role>().GetAsync(r => r.RoleID == userRole.RoleID);
                if (role != null)
                {
                    roleNames.Add(role.RoleName);
                }
            }

            userDtos.Add(new UserDtoNew
            {
                UserID = u.UserID,
                Username = u.Username,
                Email = emails != null && emails.Any() ? string.Join(", ", emails.Select(e => e.EmailAddress)) : "No Email", // Nếu không có email thì hiển thị "No Email"
                ImageUrl = u.ImageUrl ?? "No Image", // Nếu không có ảnh thì hiển thị "No Image"
                RoleName = roleNames.Any() ? string.Join(", ", roleNames) : "No Role"// Nếu không có vai trò thì hiển thị "No Role"
            });
        }

        return userDtos;
    }


    // public async Task<UserDto> GetUserByIdAsync(int id)
    // {
    //     var user = await _unitOfWork.Repository<User>().GetAsync(u => u.UserID == id);

    //     if (user == null)
    //     {
    //         throw new Exception("User not found.");
    //     }




    //     var role = await _unitOfWork.Repository<UserRole>().GetAsync(r => r.UserID == user.UserID);

    //     return new UserDto
    //     {
    //         UserID = user.UserID,
    //         Username = user.Username,
    //         Email = user.Email,
    //         ImageUrl = user.ImageUrl,
    //         RoleName = role?.Role.Name // Giả định Role có thuộc tính Name
    //     };
    // }

    // public async Task<UserDto> CreateUserAsync(CreateUserDto model)
    // {
    //     var user = new User
    //     {
    //         Username = model.Username,
    //         // Hash password here
    //         Email = model.Email,
    //         // Handle image upload here
    //     };

    //     await _unitOfWork.Repository<User>().AddAsync(user);
    //     await _unitOfWork.SaveChangesAsync();

    //     // Thêm vai trò cho người dùng
    //     var role = await _unitOfWork.Repository<Role>().GetAsync(r => r.Name == model.RoleName);
    //     if (role != null)
    //     {
    //         var userRole = new UserRole
    //         {
    //             UserID = user.UserID,
    //             RoleID = role.RoleID
    //         };

    //         await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
    //         await _unitOfWork.SaveChangesAsync();
    //     }

    //     return new UserDto
    //     {
    //         UserID = user.UserID,
    //         Username = user.Username,
    //         Email = user.Email,
    //         ImageUrl = user.ImageUrl,
    //         RoleName = role?.Name
    //     };
    // }

    // public async Task UpdateUserAsync(UpdateUserDto model)
    // {
    //     var user = await _unitOfWork.Repository<User>().GetAsync(u => u.UserID == model.UserID);
    //     if (user == null)
    //     {
    //         throw new Exception("User not found.");
    //     }

    //     user.Username = model.Username;
    //     user.Email = model.Email;
    //     // Handle image upload here if necessary

    //     await _unitOfWork.SaveChangesAsync();

    //     // Cập nhật vai trò
    //     var role = await _unitOfWork.Repository<Role>().GetAsync(r => r.RoleName == model.RoleName);
    //     var userRole = await _unitOfWork.Repository<UserRole>().GetAsync(ur => ur.UserID == user.UserID);
    //     if (userRole != null && role != null)
    //     {
    //         userRole.RoleID = role.RoleID;
    //         await _unitOfWork.SaveChangesAsync();
    //     }
    // }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _unitOfWork.Repository<User>().GetAsync(u => u.UserID == id);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        // Đặt isActive thành false
        user.isActive = false;

        // Cập nhật người dùng trong cơ sở dữ liệu
        _unitOfWork.Repository<User>().UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

    }



    public async Task<bool> UninsactiveUserAsync(int userId)
    {
        var user = await _unitOfWork.Repository<User>().GetAsync(u => u.UserID == userId);
        
        if (user == null)
        {
            return false; // Người dùng không tồn tại
        }

        user.isActive = true; // Khôi phục trạng thái thành hoạt động

        _unitOfWork.Repository<User>().UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(); // Lưu thay đổi

        return true; // Trả về true nếu thành công
    }
}
