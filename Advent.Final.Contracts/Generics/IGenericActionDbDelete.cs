
namespace Advent.Final.Contracts.Generics
{
    public interface IGenericActionDbDelete<T> where T : class
    {
        Task<bool> DeleteAsync(int id);
    }
}
