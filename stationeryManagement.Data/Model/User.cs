using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace stationeryManagement.Data.Model;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    [DataType("Nvarchar(200)")]
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Image { get; set; }
    public Guid? RoleId { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public Guid? SuperiorId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    [ForeignKey("RoleId")]
    public virtual Role? Role { get; set; }
    public virtual ICollection<Request>? Requests { get; set; }
    [ForeignKey("SuperiorId")]
    public virtual User? Superior { get; set; }
    public virtual ICollection<User>? Subordinates { get; set; }
}