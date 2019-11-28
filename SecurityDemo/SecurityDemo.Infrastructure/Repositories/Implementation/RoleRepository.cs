using SecurityDemo.Domain.Entities;
using SecurityDemo.Domain.Repositories.Interfaces;

namespace SecurityDemo.Data.Repositories.Implementation
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(ISecurityDemoContext context) 
            : base(context)
        {

        }
    }
}
