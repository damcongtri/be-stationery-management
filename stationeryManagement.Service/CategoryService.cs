using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Dto.CategoryDto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Common;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Service;

public class CategoryService : EntityService<Category>, ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.CategoryRepository)
    {
        _unitOfWork = unitOfWork;
    }

    // Create
    public async Task<Category> CreateCategory(CategoryDto categoryDto)
    {
        var entity = new Category()
        {
            CategoryName = categoryDto.CategoryName
        };
        var result = await _unitOfWork.CategoryRepository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        return result;
    }

    // Read
    public async Task<Category?> GetCategoryById(int categoryId)
    {
        return await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _unitOfWork.CategoryRepository.GetAll().ToListAsync();
    }

    // Update
    public async Task<bool> UpdateCategory(CategoryDto categoryDto, int id)
    {
        var category = new Category()
        {
            CategoryId = id,
            CategoryName = categoryDto.CategoryName
        };
        _unitOfWork.CategoryRepository.UpdateAsync(category);
        return await _unitOfWork.CommitAsync() > 0;
    }

    // Delete
    public async Task<bool> DeleteCategory(int categoryId)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
        if (category != null)
        {
           await _unitOfWork.CategoryRepository.DeleteByIdAsync(category.CategoryId);
        }

        return await _unitOfWork.CommitAsync() > 0;
    }
}