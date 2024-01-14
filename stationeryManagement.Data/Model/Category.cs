using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Model;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [DataType("nvarchar(100)")]
    public string CategoryName { get; set; }

    public virtual ICollection<Stationery>? Stationery { get; set; }
}