using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MiniStore.Dto;
using MiniStore.Models;
using MiniStore.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace MiniStore.Controllers
{



    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _logger.LogInformation($"users controller Invoked ...");
        }

        /// <summary>
        /// Retourne L'utilisateur enrégistré
        /// </summary>
        /// <returns>L'utilisateur enrégistré</returns>
        /// <response code="201">L'utilisateur est enrégistré avec succès</response>
        /// <response code="400">Cet utilisateur existe déja ou veillez saisir tous les champs</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si l'un des champs vide</exception>
        // GET: api/User/register
        [ProducesResponseType(typeof(User), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            try
            {
                _logger.LogInformation($"users api Invoked (pour enregistrer un nouveau utilisateur) ...");
                userForRegisterDto.userName = userForRegisterDto.userName.ToLower();
                if (await _userService.UserExist(userForRegisterDto.userName))
                {
                    _logger.LogWarning($"Cet utilisateur {userForRegisterDto.userName} existe déja, Veillez saisir un autre nom !");
                    return BadRequest($"Cet utilisateur {userForRegisterDto.userName}  existe déja, Veillez saisir un autre nom !");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning(ModelState.ToString());
                    return BadRequest(ModelState);
                }
                var userToCreate = new User
                {
                    UserName = userForRegisterDto.userName
                };

                var createdUser = await _userService.Register(userToCreate, userForRegisterDto.password);
                return StatusCode(201, createdUser);
            }
            catch (Exception e)
            {
                _logger.LogError($"une erreur est survenue lors de traitement de l'jout d'un nouvel utilisateur {userForRegisterDto.userName}, avec un message de : " + e.Message);
                return new NotFoundResult();
            }

        }

        /// <summary>
        /// Retourne le token d'utilisateur connecté
        /// </summary>
        /// <returns>Le token d'utilisateur connecté</returns>
        /// <response code="200">Le token d'utilisateur connecté</response>
        /// <response code="400">Cet utilisateur n'existe pas</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si l'utilisateur n'existe pas</exception>
        // GET: api/User/login
        [ProducesResponseType(typeof(ActionResult), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            try
            {
                _logger.LogInformation("users api Invoked (pour s'authentifier) ...");

                var userSelected = await _userService.Login(userForLoginDto.userName.ToLower(), userForLoginDto.password);
                if (userSelected == null)
                { 
                    return Unauthorized();
                }
                // Generate JWT Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = await _userService.GenerateToken(userSelected);
                return StatusCode(200, new
                {
                    token = tokenHandler.WriteToken(token)
                });
            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return new NotFoundResult();
            }

        }
    }
}
