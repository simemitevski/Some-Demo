using SecurityDemo.Domain.Entities;
using SecurityDemo.Domain.Repositories.Interfaces;
using System;
using System.Linq;

namespace SecurityDemo.Data.Repositories.Implementation
{
    public class UserActivationRepository : RepositoryBase<UserActivation>, IUserActivationRepository
    {
        public UserActivationRepository(ISecurityDemoContext context) 
            : base(context)
        {

        }

        public UserActivation GetByActivationCode(Guid code)
        {
            return this.DbSet.First(x => x.ActivationCode == code);
        }
    }
}
