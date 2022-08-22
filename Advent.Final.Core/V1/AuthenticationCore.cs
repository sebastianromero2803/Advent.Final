using Advent.Final.Contracts.Repository;
using Advent.Final.Core.Handlers;
using Advent.Final.Entities.DTOs;
using Advent.Final.Entities.Entities;
using Advent.Final.Entities.Utils;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Advent.Final.Core.V1
{
    public class AuthenticationCore
    {
        private readonly IUserRepository _userContext;
        private readonly UserCore _userCore;
        private readonly ErrorHandler<User> _errorHandler;
        private readonly IConfiguration _config;
        private readonly ILogger<User> _logger;
        private readonly IMapper _mapper;

        public AuthenticationCore(ILogger<User> userLogger, IMapper mapper,IConfiguration configuration, IUserRepository userContext)
        {
            _userCore = new UserCore(userLogger, mapper, userContext);
            _userContext = userContext;
            _logger = userLogger;
            _mapper = mapper;
            _errorHandler = new ErrorHandler<User>(userLogger);
            _config = configuration;
        }

        public async Task<ResponseService<UserLoginDto>> UserAuthentication(string username, string password)
        {
            try
            {
                if (await ValidateUserAccess(username, password)) {
                    List<User> users = await _userContext.GetByFilterAsync(u => u.UserName.Equals(username));
                    var response = new UserLoginDto()
                    {
                        Token = GenerarTokenJWT(users[0]),
                        UserName = username,
                        UserId = users[0].Id,
                        ExpireDate = DateTime.UtcNow.AddDays(1)
                    };
                    users[0].LastLogIn = DateTime.UtcNow;
                    await _userContext.UpdateAsync(users[0]);
                    return new ResponseService<UserLoginDto>(false, "Login Successful", HttpStatusCode.OK, response);
                }
                return new ResponseService<UserLoginDto>(true, "Login Unsuccessful", HttpStatusCode.Forbidden, new UserLoginDto());
            }
            catch (Exception e)
            {
                return _errorHandler.Error(e, "UserAuthentication", new UserLoginDto());
            }
        }

        public async Task<ResponseService<bool>> AddPassword(string username, string password)
        {
            try
            {
                bool result = await _userCore.SetPassword(username, password);
                return new ResponseService<bool>(false, "Password Added", HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return _errorHandler.Error(e, "AddPassword", false);
            }
        }

        public async Task<ResponseService<bool>> ChangePassword(string username, string password, string newPassword)
        {
            try
            {
                ResponseService<bool> result = await _userCore.ChangePasswordAsync(username, password, newPassword);
                return new ResponseService<bool>(false, "Password changed", HttpStatusCode.OK, result.Content);
            }
            catch (Exception e)
            {
                return _errorHandler.Error(e, "No password Changed", false);
            }
        }
        public async Task<bool> ValidateUserAccess(string username, string password)
        {
            EncryptCore encryptCore = new EncryptCore();
            string passEncrypt = encryptCore.Encrypt_SHA256(username, password);

            List<User> users = await _userContext.GetByFilterAsync(u => u.UserName.Equals(username));
            if (users.Count == 0 || users[0].Password != passEncrypt)
                return false;
            return true;
        }

        private string GenerarTokenJWT(User user)
        {
            // Create header
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["JWT:SecretKey"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // Create claims
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Gender, user.Gender)
            };

            // Create payload
            var _Payload = new JwtPayload(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Expired in 24 hours.
                    expires: DateTime.UtcNow.AddHours(24)
                );

            // Create a Token
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);

        }
    }
}
