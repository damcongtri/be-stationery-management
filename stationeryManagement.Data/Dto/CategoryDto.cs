using System.ComponentModel.DataAnnotations;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Data.Dto;

public class CategoryDto
{
    public int? CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; }

    public ICollection<Stationery>? Stationery { get; set; }
}