using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniStore.Controllers;
using MiniStore.Dto;
using MiniStore.Models;
using MiniStore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniStoreTest.Controllers
{
    public class ProductsCntroller
    {

        private readonly Mock<IProductService> _productService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<ProductController>> _logger;
        private readonly ProductController _productController;

        public ProductsCntroller()
        {
            _productService = new Mock<IProductService>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<ProductController>>();

            _productController = new ProductController(_productService.Object, _mapper.Object, _logger.Object);

        }





        [Fact]
        public void GetAllProducts_WhenCalled_ReturnsOkResult()
        {
            // Arrange

            // Act
            var okResult = _productController.GetAllProducts();
            // Assert
            Assert.IsType<ObjectResult>(okResult.Result as ObjectResult);
            //Assert.Equals(okResult.Value)
        }




        [Fact]
        public void GetProductByID_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            int codeProduct = 2;
            _productService.Setup(r => r.GetProductByID(It.IsAny<int>()))
                .Callback<int>(x => codeProduct = x);


            // Act
            var code = 2;
            var productReturned = _productController.GetProductByID(code);
            _productService.Verify(x => x.GetProductByID(It.IsAny<int>()), Times.Once);

            // Assert

            Assert.Equal(code, codeProduct);
        }



        [Fact]
        public void AddProduct_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            Product product = null;
            _productService.Setup(r => r.AddProduct(It.IsAny<Product>()))
                .Callback<Product>(x => product = x);


            // Act

            var productDto = new ProductDto
            {
                ProductId = 4,
                ProductName = "Test_Name",
                ProductDescription = "Test_Description",
                Productmanufacturing = DateTime.Now,
                Category_Prodcut_Id = 1
            };
            var productReturned = _productController.AddProduct(productDto);
            _productService.Verify(x => x.AddProduct(It.IsAny<Product>()), Times.Once);


            // Assert

            Assert.True(productReturned.IsCompleted);
        }






        [Fact]
        public void DeleteProduct_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            int codeProduct = 2;
            _productService.Setup(r => r.DeleteProduct(It.IsAny<int>()))
               .Callback<int>(x => codeProduct = x);


            // Act

            var code = 2;
            var operation = _productController.DeleteProduct(code);
            _productService.Verify(x => x.DeleteProduct(It.IsAny<int>()), Times.Once);


            // Assert

            Assert.True(operation.IsCompleted);
        }





        [Fact]
        public void UpdateProduct_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            Product productTransfert = null;
            ProductDto product = null;
            _productService.Setup(r => r.UpdateProduct(It.IsAny<int>(), It.IsAny<Product>()))
               .Callback<int, Product>((x, y) => productTransfert = y);


            // Act

            var code = 2;
            var operation = _productController.UpdateProduct(code, product);
            _productService.Verify(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<Product>()), Times.Once);


            // Assert

            Assert.True(operation.IsCompleted);
        }
    }

}
