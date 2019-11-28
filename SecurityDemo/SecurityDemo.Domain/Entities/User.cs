using System;
using System.Collections.Generic;

namespace SecurityDemo.Domain.Entities
{
    public class User
    {
        public User()
        {
            this.UserRoles = new HashSet<UserRole>();
            this.UserActivations = new HashSet<UserActivation>();
        }

        public User(string firstName, 
            string lastName,
            string email,
            string emb,
            string userName,
            string password,
            ICollection<Role> roles) 
            : this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Emb = emb;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = false;
            this.LastUpdateOn = DateTime.Now;

            foreach(var role in roles)
            {
                this.UserRoles.Add(new UserRole { RoleId = role.Id });
            }

            this.UserActivations.Add(new UserActivation { ActivationCode = Guid.NewGuid(), ExpiresOn = DateTime.Now.AddMinutes(30), IsActive = true });
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Emb { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdateOn { get; set; }
        public int? LastUpdateBy { get; set; }
        public DateTime? LastLoginOn { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<UserActivation> UserActivations { get; set; }
    }
}
