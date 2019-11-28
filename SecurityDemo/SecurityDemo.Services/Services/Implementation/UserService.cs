using SecurityDemo.Data;
using SecurityDemo.Domain.Entities;
using SecurityDemo.Services.Definition;
using SecurityDemo.Services.Definition.Services;
using SecurityDemo.Services.ViewModels;
using System.Collections.Generic;

namespace SecurityDemo.Services.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISecurityService securityService;

        public UserService(IUnitOfWork unitOfWork,
            ISecurityService securityService)
        {
            this.unitOfWork = unitOfWork;
            this.securityService = securityService;
        }
        public void CreateUser(UserViewModel user)
        {
            this.unitOfWork.UserRepository.Add(new User(
                user.FirstName,
                user.LastName,
                user.Email,
                user.Emb,
                user.UserName,
                this.securityService.HashPassword(user.Password),
                new List<Role> { new Role { Id = 1, Name = "SystemAdmin", IsActive = true } }
                ));

            this.unitOfWork.SaveChanges();
        }

        public User GetUserByUsername(string userName)
        {
            return this.unitOfWork.UserRepository.GetByUsername(userName);
        }
    }
}
