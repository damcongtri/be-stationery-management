using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Model;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public virtual ICollection<Stationery> Stationery { get; set; } = new List<Stationery>();
}