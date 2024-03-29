using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace stationeryManagement.Data.Model;

public class RequestDetail
{
    [Key]
    public int RequestDetailsId { get; set; }
    public int RequestId { get; set; }
    public int StationeryId { get; set; }
    public int Quantity { get; set; }
    
    [ForeignKey("RequestId")]
    [JsonIgnore]
    public virtual Request? Request { get; set; }
    [ForeignKey("StationeryId")]
    
    public virtual Stationery? Stationery { get; set; }
}
