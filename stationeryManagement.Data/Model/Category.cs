using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace stationeryManagement.Data.Model;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [MaxLength(150)]
    public string CategoryName { get; set; }
    public bool Deleted { get; set; } = false;
    
    [JsonIgnore]
    public virtual ICollection<Stationery>? Stationery { get; set; }
}