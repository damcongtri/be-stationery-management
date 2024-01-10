using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stationeryManagement.Data.Model;

public class User
{
    [Key]
    public int UserId { get; set; }
    [DataType("Nvarchar(200)")]
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Image { get; set; }
    public string Password { get; set; }
    public int? SuperiorId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastLoginDate { get; set; }

    public virtual Role? Role { get; set; }
    public virtual ICollection<Request>? Requests { get; set; }
    [ForeignKey("SuperiorId")]
    public virtual User? Superior { get; set; }
    // [ForeignKey("SuperiorId")]
    public virtual ICollection<User>? Subordinates { get; set; }
}