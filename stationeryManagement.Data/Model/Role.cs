using System.ComponentModel.DataAnnotations;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Data;

public class Role
{
    [Key]
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public decimal ThresholdAmountPerMonth { get; set; }
    
    public virtual ICollection<User> Users { get; set; }
}