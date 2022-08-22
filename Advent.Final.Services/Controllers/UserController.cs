using Advent.Final.Contracts.Repository;
using Advent.Final.Core.V1;
using Advent.Final.Entities.DTOs;
using Advent.Final.Entities.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Advent.Final.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserCore _userCore;

        public UserController(ILogger<User> logger, IMapper mapper, IUserRepository context)
        {
            _userCore = new UserCore(logger, mapper, context);
        }

        // POST api/<UserController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserCreateDto user)
        {
            var response = await _userCore.CreateUserAsync(user);
            return StatusCode((int)response.StatusHttp, response);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<bool>> GetUsers()
        {
            var response = await _userCore.GetUsersAsync();
            return StatusCode((int)response.StatusHttp, response);
        }

    }
}
