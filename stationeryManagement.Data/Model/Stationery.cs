using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stationeryManagement.Data.Model;

public class Stationery
{
    [Key]
    public int StationeryId { get; set; }
    [MaxLength(250)]
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string UnitPrice { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public int? SupplierId { get; set; }
    public bool Deleted { get; set; } = false;

    [ForeignKey("SupplierId")]
    public virtual Supplier? Supplier { get; set; }
    [ForeignKey("CategoryId")]
    public virtual Category? Category { get; set; }
}