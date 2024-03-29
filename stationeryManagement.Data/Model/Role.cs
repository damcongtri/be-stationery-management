using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Data;

public class Role
{
    [Key]
    public Guid RoleId { get; set; }
    [MaxLength(100)]
    public string RoleName { get; set; }
    public decimal ThresholdAmountPerMonth { get; set; }
    [JsonIgnore]
    public virtual ICollection<User>? Users { get; set; }
}