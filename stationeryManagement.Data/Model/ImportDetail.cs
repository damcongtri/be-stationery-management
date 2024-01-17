using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace stationeryManagement.Data.Model;

public class ImportDetail
{
    [Key]
    public int ImportDetailsId { get; set; }
    public int ImportId { get; set; }
    public int StationeryId { get; set; }
    public int Quantity { get; set; }
        
    [ForeignKey("ImportId")]
    [JsonIgnore]
    public virtual Import Import { get; set; }
    [ForeignKey("StationeryId")]
    public virtual Stationery Stationery { get; set; }
}