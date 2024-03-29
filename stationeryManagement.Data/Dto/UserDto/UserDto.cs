using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Dto.UserDto;

public class UserDto
{
    public Guid? UserId { get; set; }
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [FileExtensions(Extensions = "jpg, jpeg, gif, png, webp", ErrorMessage = "The file extension is not allowed.")]
    public string? FileName => FileUpload?.FileName;
    public IFormFile? FileUpload { get; set; }
    public string? Image { get; set; }
    public string? Password { get; set; }
    public Guid? SuperiorId { get; set; }
    public Guid? RoleId { get; set; }
}