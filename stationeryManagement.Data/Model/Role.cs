using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data;

public class Role
{
    [Key]
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public decimal ThresholdAmountPerMonth { get; set; }
}