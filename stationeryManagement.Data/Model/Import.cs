using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Model;

public class Import
{
    [Key]
    public int ImportId { get; set; }
    public int UserCreateId { get; set; }
    public DateTime ImportDate { get; set; }

    public virtual User UserCreate { get; set; }
    public virtual ICollection<ImportDetail> ImportDetails { get; set; }
}