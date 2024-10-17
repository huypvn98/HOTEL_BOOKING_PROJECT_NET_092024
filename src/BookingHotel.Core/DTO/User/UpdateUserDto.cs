using Microsoft.AspNetCore.Http;

public class UpdateUserDto
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string RoleName { get; set; }
    public IFormFile Image { get; set; }
}