using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniStore.Controllers;
using MiniStore.Dto;
using MiniStore.Models;
using MiniStore.Repositories;
using MiniStore.Services;
using MiniStoreTest.Helper;
using Moq;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniStoreTest.Services
{
    public class CategoriesServiceTest
    {

        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CategoryService>> _logger;


        public CategoriesServiceTest()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<CategoryService>>();
        }


        [Fact]
        public async void GetAllCategories_WhenCalled_ReturnsResult()
        {
            // Arrange
            _mockCategoryRepository.Setup(r => r.GetAllCategories()).ReturnsAsync(CategoryHelper.CreateCategoryListForTest());
            CategoryService _categoryService = new CategoryService(_mockCategoryRepository.Object, _mockMapper.Object, _logger.Object);


            // Act
            var Result = await _categoryService.GetAllCategories();
            var response = Assert.IsType<List<CategoryDto>>(Result);

            // Assert
            _mockCategoryRepository.Verify(x => x.GetAllCategories(), Times.Once);
            Assert.NotNull(response);
        }

        [Fact]
        public async void GetCatgoryByID_WhenCalled_ReturnsResult()
        {
            // Arrange
            _mockCategoryRepository.Setup(r => r.GetCatgoryByID(It.IsAny<int>())).ReturnsAsync(CategoryHelper.CreateCategoryForTest());
            _mockMapper.Setup(r => r.Map<CategoryDto>(It.IsAny<Category>())).Returns(CategoryHelper.CreateCategoryDtoForTest());
            CategoryService _categoryService = new CategoryService(_mockCategoryRepository.Object, _mockMapper.Object, _logger.Object);


            // Act
            var code = 1;
            var Result = await _categoryService.GetCatgoryByID(code);
            var response = Assert.IsType<CategoryDto>(Result);

            // Assert
            _mockCategoryRepository.Verify(x => x.GetCatgoryByID(It.IsAny<int>()), Times.Once);
            Assert.NotNull(response);
            Assert.Equal(code, response.CategoryId);
        }



        [Fact]
        public async void DeleteCategory_WhenCalled_ReturnsResult()
        {
            // Arrange
            _mockCategoryRepository.Setup(r => r.DeleteCategory(It.IsAny<int>())).ReturnsAsync(true);
            CategoryService _categoryService = new CategoryService(_mockCategoryRepository.Object, _mockMapper.Object, _logger.Object);


            // Act
            var Result = await _categoryService.DeleteCategory(2);
            var response = Assert.IsType<bool>(Result);

            // Assert
            _mockCategoryRepository.Verify(x => x.DeleteCategory(It.IsAny<int>()), Times.Once);
            Assert.True(Result);
        }


        [Fact]
        public async void AddCategory_WhenCalled_ReturnsResult()
        {
            // Arrange  
            Category CreateCategory = CategoryHelper.CreateCategoryForAddMethodServiceTest();
            var exceptedCategory = CategoryHelper.CreateCategoryForTest();
            _mockCategoryRepository.Setup(r => r.AddCategory(It.IsAny<Category>())).ReturnsAsync(exceptedCategory);
            _mockMapper.Setup(r => r.Map<CategoryDto>(It.IsAny<Category>())).Returns(CategoryHelper.CreateCategoryDtoForTest());
            CategoryService _categoryService = new CategoryService(_mockCategoryRepository.Object, _mockMapper.Object, _logger.Object);


            // Act
            var Result = await _categoryService.AddCategory(CreateCategory);
            var response = Assert.IsType<CategoryDto>(Result);

            // Assert

            CategoryDto createdCategory = Assert.IsAssignableFrom<CategoryDto>(response);
            Assert.NotNull(Result);
            Assert.Equal(exceptedCategory.CategoryName, createdCategory.CategoryName);
            _mockCategoryRepository.Verify(x => x.AddCategory(It.IsAny<Category>()), Times.Once);
          
        }


        [Fact]
        public async void UpdateCategory_WhenCalled_ReturnsResult()
        {
            // Arrange  
            Category CreateCategory = CategoryHelper.CategoryTransfertForUpdateServiceMethodTest();
            var exceptedCategory = CategoryHelper.UpdateCategoryForUpdateServiceMethodTest();
            _mockCategoryRepository.Setup(r => r.UpdateCategory(It.IsAny<int>(),It.IsAny<Category>())).ReturnsAsync(exceptedCategory);
            _mockMapper.Setup(r => r.Map<CategoryDto>(It.IsAny<Category>())).Returns(CategoryHelper.CreateCategoryDtoForUpdateTest());
            CategoryService _categoryService = new CategoryService(_mockCategoryRepository.Object, _mockMapper.Object, _logger.Object);


            // Act
            var code = 2;
            var Result = await _categoryService.UpdateCategory(code,CreateCategory);
            var response = Assert.IsType<CategoryDto>(Result);

            // Assert

            CategoryDto createdCategory = Assert.IsAssignableFrom<CategoryDto>(response);
            Assert.NotNull(Result);
            Assert.Equal(exceptedCategory.CategoryName, Result.CategoryName);
            _mockCategoryRepository.Verify(x => x.UpdateCategory(It.IsAny<int>(), It.IsAny<Category>()), Times.Once);

        }




    }

}
