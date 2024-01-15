using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stationeryManagement.Data.Model;

public class RequestDetail
{
    [Key]
    public int RequestDetailsId { get; set; }
    public int RequestId { get; set; }
    public int StationeryId { get; set; }
    public int Quantity { get; set; }
    
    [ForeignKey("RequestId")]
    public virtual Request Request { get; set; }
    [ForeignKey("StationeryId")]
    public virtual Stationery Stationery { get; set; }
}
