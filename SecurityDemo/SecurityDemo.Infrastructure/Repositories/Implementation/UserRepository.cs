using SecurityDemo.Domain.Entities;
using SecurityDemo.Domain.Repositories.Interfaces;
using System.Linq;

namespace SecurityDemo.Data.Repositories.Implementation
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ISecurityDemoContext context)
            : base(context)
        {
            
        }

        public User GetByUsername(string userName)
        {
            return this.DbSet.First(x => x.UserName == userName);
        }
    }
}
