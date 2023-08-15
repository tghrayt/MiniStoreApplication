﻿using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Repositories
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<Category> GetCatgoryByID(int categoryId);
        public Task<bool> DeleteCategory(int categoryId);
        public Task<Category> AddCategory(Category category);
        public Task<Category> UpdateCategory(int categoryId, Category category);
    }
}
