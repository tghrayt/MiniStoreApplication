using AutoMapper;
using Microsoft.Extensions.Logging;
using MiniStore.Dto;
using MiniStore.Models;
using MiniStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }




        public async Task<CategoryDto> AddCategory(Category category)
        {
            try
            {
                var categoryResult = await _categoryRepository.AddCategory(category);
                return _mapper.Map<CategoryDto>(categoryResult);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Une erreur est survenue lors d'ajout d'une nouvelle catégorie, avec le message d'exception : {ex.Message}");
                throw;
            }
           
        }




        public async Task<bool> DeleteCategory(int categoryId)
        {
            try
            {
                return await _categoryRepository.DeleteCategory(categoryId);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Une erreur est survenue lors de supression de la catégorie d'id = {categoryId}, avec le message d'exception : {ex.Message}");
                throw;
            }
        }




        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            try
            {
                IList<CategoryDto> categoryDtos = new List<CategoryDto>();
                var categoryResult = await _categoryRepository.GetAllCategories();
                foreach (var category in categoryResult)
                {
                    categoryDtos.Add(_mapper.Map<CategoryDto>(category));
                }
                return categoryDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Une erreur est survenue lors de la récupération de la liste des catégories , avec le message d'exception : {ex.Message}");
                throw;
            }
            
        }



        public async Task<CategoryDto> GetCatgoryByID(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetCatgoryByID(categoryId);
                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Une erreur est survenue lors de la récupération de la catégorie d'id = {categoryId}, avec le message d'exception : {ex.Message}");
                throw;
            }
            
        }




        public async Task<CategoryDto> UpdateCategory(int categoryId, Category category)
        {
            try
            {
                var categoryUpdated = await _categoryRepository.UpdateCategory(categoryId, category);
                return _mapper.Map<CategoryDto>(categoryUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Une erreur est survenue lors de la modification de la catégorie d'id = {categoryId}, avec le message d'exception : {ex.Message}");
                throw;
            }
            
        }

     
    }
}
