using Magalog.Domain.Entitites;

namespace Magalog.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task Add(List<User> users);
}
