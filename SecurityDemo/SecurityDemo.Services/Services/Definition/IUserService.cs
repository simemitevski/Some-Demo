using SecurityDemo.Domain.Entities;
using SecurityDemo.Services.ViewModels;

namespace SecurityDemo.Services.Definition.Services
{
    public interface IUserService
    {
        void CreateUser(UserViewModel user);
        User GetUserByUsername(string userName);
    }
}
