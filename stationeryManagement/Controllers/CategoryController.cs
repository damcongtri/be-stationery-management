using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Dto.CategoryDto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/Category
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategory()
        {
            return await _categoryService.GetAllCategories();
        }

        // GET: api/Category/5
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return Ok(await _categoryService.GetCategoryById(id));
        }

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto category)
        {
            var result = await _categoryService.CreateCategory(category);
            return  Ok(result);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDto category)
        {
            var result = await _categoryService.UpdateCategory(category,id);
            return  Ok(result);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return  Ok(await _categoryService.DeleteCategory(id));
        }
    }
}
