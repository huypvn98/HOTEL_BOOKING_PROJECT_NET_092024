using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookingHotel.Core.DTO;
using Swashbuckle.AspNetCore.Annotations;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    /// <summary>
    /// Registers a new customer.
    /// </summary>
    /// <param name="model">The registration details.</param>
    /// <returns>A message indicating the result of the registration.</returns>
    [HttpPost("register")]
    [SwaggerOperation(Summary = "Registers a new user", Description = "Registers a new user with the provided details.")]
    [SwaggerResponse(200, "Registration successful", typeof(object))]
    [SwaggerResponse(400, "Registration failed", typeof(object))]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        try
        {
            var message = await _authService.RegisterAsync(model);
            return Ok(new { message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        try
        {
            var tokens = await _authService.LoginAsync(model);
            return Ok(new
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                UserInfo = tokens.UserInfo // Đảm bảo rằng thông tin người dùng cũng được trả về
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }
}
