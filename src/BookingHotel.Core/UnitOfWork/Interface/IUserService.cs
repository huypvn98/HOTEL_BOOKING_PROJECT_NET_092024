using BackendAPIBookingHotel.Model;

public interface IUserService
{
    Task<IEnumerable<UserDtoNew>> GetAllUsersAsync();
    Task<bool> UninsactiveUserAsync(int userId); // Thêm phương thức này
    Task DeleteUserAsync(int id);
    // Các phương thức khác...
}
