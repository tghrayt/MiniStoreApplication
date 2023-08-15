using AutoMapper;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniStore.Context;
using MiniStore.Controllers;
using MiniStore.Dto;
using MiniStore.Models;
using MiniStore.Services;
using MiniStoreTest.Helper;
using Moq;
using Nest;
using Xunit;

namespace MiniStoreTest.Controllers
{
    public class CategoriesControllerTest
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CategoryController>> _logger;

        public CategoriesControllerTest()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _mockMapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<CategoryController>>();
        }

        [Fact]
        public async void GetAllCategories_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockCategoryService.Setup(r => r.GetAllCategories()).ReturnsAsync(CategoryHelper.CreateCategoryDtoListForTest());
            CategoryController _categoryController = new CategoryController(_mockCategoryService.Object, _mockMapper.Object, _logger.Object);

            // Act
            var okResult = await _categoryController.GetAllCategories();
            OkObjectResult response = Assert.IsType<OkObjectResult>(okResult);

            // Assert
            Assert.NotNull(response.Value);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }


        [Fact]
        public async void GetCatgoryByID_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockCategoryService.Setup(r => r.GetCatgoryByID(It.IsAny<int>())).ReturnsAsync(CategoryHelper.CreateCategoryDtoForTest());
            CategoryController _categoryController = new CategoryController(_mockCategoryService.Object, _mockMapper.Object, _logger.Object);

            // Act
            var code = 1;
            var okResult = await _categoryController.GetCatgoryByID(code);
            OkObjectResult response = Assert.IsType<OkObjectResult>(okResult);


            // Assert
            _mockCategoryService.Verify(x => x.GetCatgoryByID(It.IsAny<int>()), Times.Once);
            Assert.NotNull(response.Value);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }

        [Fact]
        public async void AddCategory_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            CategoryDto CreateCategory = CategoryHelper.CreateCategoryForAddMethodTest();
            var exceptedCategoryDto = CategoryHelper.CreateCategoryDtoForTest();
            _mockCategoryService.Setup(r => r.AddCategory(It.IsAny<Category>())).ReturnsAsync(exceptedCategoryDto);
            CategoryController _categoryController = new CategoryController(_mockCategoryService.Object, _mockMapper.Object, _logger.Object);

            // Act
            var okResult = await _categoryController.AddCategory(CreateCategory);
            ObjectResult response = Assert.IsType<ObjectResult>(okResult);

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
            CategoryDto createdCategoryDto = Assert.IsAssignableFrom<CategoryDto>(response.Value);
            Assert.Equal(exceptedCategoryDto.CategoryName,createdCategoryDto.CategoryName);
            _mockCategoryService.Verify(x => x.AddCategory(It.IsAny<Category>()), Times.Once);

        }


        [Fact]
        public async void DeleteCategory_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockCategoryService.Setup(r => r.DeleteCategory(It.IsAny<int>())).ReturnsAsync(true);
            CategoryController _categoryController = new CategoryController(_mockCategoryService.Object, _mockMapper.Object, _logger.Object);

            // Act
            var code = 2;
            var operation = await _categoryController.DeleteCategory(code);
            OkObjectResult response = Assert.IsType<OkObjectResult>(operation);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            _mockCategoryService.Verify(x => x.DeleteCategory(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void UpdateCategory_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            CategoryDto exceptedCategori = CategoryHelper.UpdateCategoryForMethodTest();
            var categoryTransfert = CategoryHelper.CategoryTransfertForUpdateMethodTest();
            _mockCategoryService.Setup(r => r.UpdateCategory(It.IsAny<int>(), It.IsAny<Category>())).ReturnsAsync(exceptedCategori);
            CategoryController _categoryController = new CategoryController(_mockCategoryService.Object, _mockMapper.Object, _logger.Object);



            // Act
            var code = 2;
            var operation = await _categoryController.UpdateCategory(code, categoryTransfert);
            OkObjectResult response = Assert.IsType<OkObjectResult>(operation);


            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            CategoryDto createdCategoryDto = Assert.IsAssignableFrom<CategoryDto>(response.Value);
            Assert.Equal(createdCategoryDto.CategoryName, exceptedCategori.CategoryName);
            _mockCategoryService.Verify(x => x.UpdateCategory(It.IsAny<int>(), It.IsAny<Category>()), Times.Once);

        }




    }
}
