using SecurityDemo.Domain.Entities;
using System.Collections.Generic;

namespace SecurityDemo.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IList<User> GetAll();

        User GetById(int id);

        void Add(User user);

        User GetByUsername(string userName);
    }
}
