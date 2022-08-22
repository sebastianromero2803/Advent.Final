using Advent.Final.Contracts.Generics;
using Advent.Final.Entities.Entities;

namespace Advent.Final.Contracts.Repository
{
    public interface IUserRepository : IGenericActionDbAdd<User>, IGenericActionDbUpdate<User>, IGenericActionDbQuery<User>
    {
    }
}
