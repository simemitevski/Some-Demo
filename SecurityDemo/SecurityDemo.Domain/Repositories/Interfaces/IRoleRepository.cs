using SecurityDemo.Domain.Entities;
using System.Collections.Generic;

namespace SecurityDemo.Domain.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        IList<Role> GetAll();

        Role GetById(int id);

        void Add(Role role);

        void Delete(Role role);
    }
}
