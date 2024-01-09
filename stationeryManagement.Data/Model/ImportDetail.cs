using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stationeryManagement.Data.Model;

public class ImportDetail
{
    [Key]
    public int ImportDetailsId { get; set; }
    public int ImportId { get; set; }
    public int StationeryId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }

    
    public virtual Import Import { get; set; }
    [ForeignKey("StationeryId")]
    public virtual Stationery Stationery { get; set; }
}