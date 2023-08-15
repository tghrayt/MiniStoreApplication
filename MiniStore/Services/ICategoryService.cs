using MiniStore.Dto;
using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDto>> GetAllCategories();
        public Task<CategoryDto> GetCatgoryByID(int categoryId);
        public Task<bool> DeleteCategory(int categoryId);
        public Task<CategoryDto> AddCategory(Category category);
        public Task<CategoryDto> UpdateCategory(int categoryId, Category category);
    }
}
