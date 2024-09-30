using BookingHotel.Core.DTO;
using BackendAPIBookingHotel.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using BookingHotel.Core;


public class AuthService
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

       
        var hashedPassword = HashPassword(model.Password);
        
   
        var user = new User
        {
            UserID = person.PersonID, 
            Username = model.UserName,
            PasswordHash = hashedPassword,
            PasswordSalt = hashedPassword,
            CreateDate = DateTime.UtcNow 
        };
        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

         var customer = new Customer
        {
            CustomerID = person.PersonID, 
            RegistrationDate = DateTime.UtcNow,
            CustomerSpecificInfo = "",
            Person = person
        };
        await _unitOfWork.Repository<Customer>().AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();


        return "User registered successfully!";
    }

    private string HashPassword(string password)
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

        return hashed;
    }
}
