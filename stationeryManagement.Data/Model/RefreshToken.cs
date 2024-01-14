using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stationeryManagement.Data.Model;

public class RefreshToken
{
    [Key]
    public int TokenId { get; set; }
    public Guid UserId{ get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
}