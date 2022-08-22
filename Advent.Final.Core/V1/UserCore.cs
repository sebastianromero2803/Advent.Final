using Advent.Final.Contracts.Repository;
using Advent.Final.Core.Handlers;
using Advent.Final.Entities.DTOs;
using Advent.Final.Entities.Entities;
using Advent.Final.Entities.Utils;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Advent.Final.Core.V1
{
    public class UserCore
    {
        private readonly IUserRepository _context;
        private readonly ErrorHandler<User> _errorHandler;
        private readonly ILogger<User> _logger;
        private readonly IMapper _mapper;


        public UserCore(ILogger<User> logger, IMapper mapper, IUserRepository context)
        {
            _logger = logger;
            _mapper = mapper;
            _errorHandler = new ErrorHandler<User>(logger);
            _context = context;
        }

        public async Task<ResponseService<User>> CreateUserAsync(UserCreateDto userCreate) {
            try
            {
                User newUser = _mapper.Map<User>(userCreate);
                newUser.CreatedAt = DateTime.UtcNow;
                newUser.Status = "Created";
                
                if (await SetPassword(userCreate.UserName, userCreate.Password)) {
                    var response = await _context.AddAsync(newUser);
                    return new ResponseService<User>(false, response == null ? "Couldn't Create The User" : "User created", HttpStatusCode.OK, response.Item1);
                }
                return new ResponseService<User>(false, "Couldn't Create The User", HttpStatusCode.OK, new User());
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "CreateUser", new User());
            }
        }

        public async Task<ResponseService<List<User>>> GetUsersAsync()
        {
            try
            {
                List<User> response = await _context.GetAllAsync();
                return new ResponseService<List<User>>(false, response == null ? "No records found" : "User list", HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "ChangePasswordAsync", new List<User>());
            }

        }

        public async Task<ResponseService<bool>> ChangePasswordAsync(string username, string password, string newPassword)
        {
            try
            {
                var users = await _context.GetByFilterAsync(u => u.UserName.Equals(username));
                if (users.Count == 0)
                    return new ResponseService<bool>(false, "Couldn't Find The User", HttpStatusCode.OK, false);
                EncryptCore encryptCore = new EncryptCore();
                string passwordAttempt = encryptCore.Encrypt_SHA256(username, password);
                if (passwordAttempt != users[0].Password)
                    return new ResponseService<bool>(false, "Incorrect Password", HttpStatusCode.OK, false);
                users[0].Password = encryptCore.Encrypt_SHA256(username, newPassword);
                await _context.UpdateAsync(users[0]);
                return new ResponseService<bool>(false, "Password Changed", HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "ChangePasswordAsync", false);
            }
        }

        public async Task<bool> SetPassword(string username, string password)
        {
            var users = await _context.GetByFilterAsync(u => u.UserName.Equals(username));
            if (users.Count == 0)
                return false;
            EncryptCore encryptCore = new EncryptCore();
            users[0].Password = encryptCore.Encrypt_SHA256(username, password);
            await _context.UpdateAsync(users[0]);
            return true;
        }
    }
}
