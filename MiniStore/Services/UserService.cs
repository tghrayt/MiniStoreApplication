using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniStore.Models;
using MiniStore.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public Task<SecurityToken> GenerateToken(User userSelected)
        {
            var claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, userSelected.UseryId.ToString()),
                new Claim(ClaimTypes.Name, userSelected.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return Task.Run(() => tokenHandler.CreateToken(tokenDescriptor)); 
        }

        public async Task<User> Login(string UserName, string Password)
        {
            return await _userRepository.Login(UserName, Password);
        }

        public async Task<User> Register(User user, string password)
        {
            return await _userRepository.Register(user, password);
        }

        public async Task<bool> UserExist(string UserName)
        {
            return await _userRepository.UserExist(UserName);
        }
    }
}
