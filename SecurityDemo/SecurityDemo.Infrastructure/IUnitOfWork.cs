using SecurityDemo.Domain.Repositories.Interfaces;

namespace SecurityDemo.Data
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        //repositories
        IUserRepository UserRepository { get; }

        IRoleRepository RoleRepository { get; }

        IUserActivationRepository UserActivationRepository { get; }
    }
}
