using BookingHotel.Core;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // 1. Get All Users
        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // 5. Delete User
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok("User Deleted successfully!!!");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // 6. Uninsactive User
        [HttpPut("{id}/active")]
        public async Task<IActionResult> UninsactiveUser(int id)
        {
            var result = await _userService.UninsactiveUserAsync(id);
            if (!result)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy người dùng
            }

            return Ok("Active user successfully!!!"); 
        }
    }
}
