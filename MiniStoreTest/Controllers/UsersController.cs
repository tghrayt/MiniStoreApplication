using AutoMapper;
using Microsoft.Extensions.Logging;
using MiniStore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;
using MiniStore.Controllers;
using MiniStore.Models;
using MiniStore.Dto;

namespace MiniStoreTest.Controllers
{
    public class UsersController
    {
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<UserController>> _logger;
        private readonly UserController _userController;
        private readonly Mock<IConfiguration> _configuration;
        public UsersController()
        {

            _userService = new Mock<IUserService>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<UserController>>();
            _configuration = new Mock<IConfiguration>();

            _userController = new UserController(_userService.Object, _mapper.Object, _logger.Object, _configuration.Object);

        }


        [Fact]
        public void Register_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            User user = null;
            _userService.Setup(r => r.Register(It.IsAny<User>(), It.IsAny<string>()))
                .Callback<User, string>((x, y) => user = x);


            // Act

            var userForRegisterDto = new UserForRegisterDto
            {
                userName = "youssef",
                password = "1234"
            };
            var userReturned = _userController.Register(userForRegisterDto);
            _userService.Verify(x => x.Register(It.IsAny<User>(), It.IsAny<string>()), Times.Once);


            // Assert

            Assert.True(userReturned.IsCompleted);
        }






        [Fact]
        public void Login_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            string userName = string.Empty;
            _userService.Setup(r => r.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((x, y) => userName = x);


            // Act

            var userForLoginDto = new UserForLoginDto
            {
                userName = "youssef",
                password = "1234"
            };
            var userReturned = _userController.Login(userForLoginDto);
            _userService.Verify(x => x.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once);


            // Assert

            Assert.True(userReturned.IsCompleted);
        }
    }
}
