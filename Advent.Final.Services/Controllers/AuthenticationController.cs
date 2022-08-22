using Advent.Final.Contracts.Repository;
using Advent.Final.Core.V1;
using Advent.Final.Entities.DTOs;
using Advent.Final.Entities.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advent.Final.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationCore _authCore;

        public AuthenticationController(ILogger<User> logger, IMapper mapper, IUserRepository context, IConfiguration configuration)
        {
            _authCore = new AuthenticationCore(logger, mapper, configuration, context);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var response = await _authCore.UserAuthentication(loginRequest.UserName, loginRequest.Password);
            return StatusCode((int)response.StatusHttp, response);
        }

        [HttpPut("addpassword")]
        [Authorize]
        public async Task<IActionResult> AddPassword(LoginRequest loginRequest)
        {
            var result = await _authCore.AddPassword(loginRequest.UserName, loginRequest.Password);
            return StatusCode((int)result.StatusHttp, result);
        }

        [HttpPut("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(LoginChangeRequest loginChangeRequest)
        {
            var result = await _authCore.ChangePassword(loginChangeRequest.UserName, loginChangeRequest.OldPassword, loginChangeRequest.NewPassword);
            return StatusCode((int)result.StatusHttp, result);
        }

    }
}
