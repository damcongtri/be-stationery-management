using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Dto.CategoryDto;

public class CategoryDto
{
    public int? CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; }
}