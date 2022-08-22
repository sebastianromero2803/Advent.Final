using Advent.Final.Contracts.Repository;
using Advent.Final.DataAccess.Context;
using Advent.Final.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Advent.Final.Repositories.ImplementRepositories
{
    public class UserRepositoryMySql : IUserRepository
    {
        private readonly MySqlContext _context;

        public UserRepositoryMySql()
        {
            _context = new MySqlContext();
        }

        public async Task<Tuple<User, bool>> AddAsync(User entity)
        {
            try
            {
                var result = _context.Users.Add(entity);
                await _context.SaveChangesAsync();
                return Tuple.Create(result.Entity, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                var result = await _context.Users.ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<User>> GetByFilterAsync(Expression<Func<User, bool>> expressionFilter = null)
        {
            try
            {
                return await _context.Users.Where<User>(expressionFilter).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                var result = await _context.Users.FindAsync(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            try
            {
                var result = _context.Users.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
