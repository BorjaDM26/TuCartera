using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController(
            IAdapter adapter,
            IUsersService usersService,
            IMapper mapper
        ) {
            _adapter = adapter;
            _usersService = usersService;
            _mapper = mapper;
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
            catch (Exception e)
            {
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
            } else {
                return BadRequest();
            }
        }

        #endregion
    }
}
