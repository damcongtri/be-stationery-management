using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stationeryManagement.Data.Model;

public class Stationery
{
    [Key]
    public int StationeryId { get; set; }
    [DataType("nvarchar(250)")]
    public string Name { get; set; }
    [DataType("Nvarchar(max)")]
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string UnitPrice { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public int ReorderLevel { get; set; }
    public int? SupplierId { get; set; }

    [ForeignKey("SupplierId")]
    public virtual Supplier? Supplier { get; set; }
}