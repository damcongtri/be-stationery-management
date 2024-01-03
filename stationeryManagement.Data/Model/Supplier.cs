using System.ComponentModel.DataAnnotations;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Data;

public class Supplier
{
    [Key]
    public int SupplierId { get; set; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    public virtual ICollection<Stationery> StationeryItems { get; set; }
}