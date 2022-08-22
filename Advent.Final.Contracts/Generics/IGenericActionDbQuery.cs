using System.Linq.Expressions;

namespace Advent.Final.Contracts.Generics
{
    public interface IGenericActionDbQuery<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> expressionFilter = null);
    }
}
