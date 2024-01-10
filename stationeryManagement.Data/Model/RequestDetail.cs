using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Model;

public class RequestDetail
{
    [Key]
    public int RequestDetailsId { get; set; }
    public int RequestId { get; set; }
    public int StationeryId { get; set; }
    public int Quantity { get; set; }
    
    public virtual Request Request { get; set; }
    public virtual Stationery Stationery { get; set; }
}
