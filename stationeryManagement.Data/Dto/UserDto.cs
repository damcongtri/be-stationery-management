using System.Text.Json.Serialization;

namespace stationeryManagement.Data.Dto;

public class UserDto
{
    public Guid? UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IFormFile? FileUpload { get; set; }
    public string? Image { get; set; }
    public string Password { get; set; }
    public Guid? SuperiorId { get; set; }
    public Guid? RoleId { get; set; }
}