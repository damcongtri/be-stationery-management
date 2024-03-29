using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Data;

public class Supplier
{
    [Key]
    public int SupplierId { get; set; }
    [DataType("Nvarchar(250)")]
    public string Name { get; set; }
    [DataType("Nvarchar(250)")]
    public string ContactInfo { get; set; }
    public bool Deleted { get; set; } = false;
    
    [JsonIgnore]
    public virtual ICollection<Stationery>? StationeryItems { get; set; }
}