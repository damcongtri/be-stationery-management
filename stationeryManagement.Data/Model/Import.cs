using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stationeryManagement.Data.Model;

public class Import
{
    [Key]
    public int ImportId { get; set; }
    public int UserCreateId { get; set; }
    public DateTime ImportDate { get; set; } = DateTime.Now;
    
    [ForeignKey("UserCreateId")]
    public virtual User UserCreate { get; set; }
    public virtual ICollection<ImportDetail>? ImportDetails { get; set; }
}