using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TuCartera.DBModel;
using TuCartera.DBModel.Contexts.Entities;
using TuCartera.Models;
using TuCartera.Services;

namespace TuCartera.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Initialization

        private readonly IAdapter _adapter;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IAdapter adapter,
            IUsersService usersService,
            IMapper mapper,
            ILogger<UsersController> logger
        ) {
            _adapter = adapter;
            _usersService = usersService;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Authentication

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult GetLogin()
        {
            try
            {
                var userId = _usersService.getLoggedUserId();

                SpUserGetLoginResult res = _adapter.GetLogin(userId.Value);
                UserDTO user = _mapper.Map<UserDTO>(res);
                return Ok(user);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> PostLogin([FromBody] LoginParameters param)
        {
            var userId = _adapter.PostLogin(param.Email, param.Password);
            if (userId != -1)
            {
                await _usersService.doLogin(userId, param.Email);
                return NoContent();
            }
            else
            {
                _logger.LogError("PostLogin: User could not be logged in");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _usersService.doLogout();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Logout: User could not be logged out");
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterParameters param)
        {
            var userId = _adapter.Register(param.Name, param.Email, param.Password);
            if(userId != -1) {
                await _usersService.doLogin(userId, param.Email);
                return NoContent();
            } else
            {
                _logger.LogError("Register: User could not be registered");
                return BadRequest();
            }
        }

        #endregion
    }
}
