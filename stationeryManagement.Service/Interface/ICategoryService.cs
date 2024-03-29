using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Dto.CategoryDto;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Service.Interface;

public interface ICategoryService 
{
    // Create
    Task<Category> CreateCategory(CategoryDto category);

    // Read
    Task<Category?> GetCategoryById(int categoryId);
    Task<IEnumerable<Category>> GetAllCategories();

    // Update
    Task<bool> UpdateCategory(CategoryDto category, int id);

    // Delete
    Task<bool> DeleteCategory(int categoryId);
}