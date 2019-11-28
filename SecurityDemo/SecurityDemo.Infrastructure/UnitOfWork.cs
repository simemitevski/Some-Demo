using System;
using SecurityDemo.Domain.Repositories.Interfaces;

namespace SecurityDemo.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ISecurityDemoContext context;

        public UnitOfWork(ISecurityDemoContext context,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserActivationRepository userActivationRepository)
        {
            this.context = context;
            this.UserRepository = userRepository;
            this.RoleRepository = roleRepository;
            this.UserActivationRepository = userActivationRepository;
        }

        public IUserRepository UserRepository { get; }

        public IRoleRepository RoleRepository { get; }

        public IUserActivationRepository UserActivationRepository { get; }

        public void Dispose()
        {
            context.Dispose();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
