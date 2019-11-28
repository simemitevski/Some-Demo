using SecurityDemo.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SecurityDemo.Domain.DataObjects
{
    public class IdentityData
    {
        public IdentityData(User user, string usersIpAddress)
        {
            this.Token = "";
            this.Id = user.Id;
            this.IsSystemAdmin = true;
            this.LastLoginDateTime = DateTime.Now.ToString();
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.RoleNames = new List<string> { "SystemAdmin", "Admin" };
            this.IpAddress = usersIpAddress;
        }

        public string Token { get; set; }

        public int Id { get; set; }

        public bool IsSystemAdmin { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LastLoginDateTime { get; set; }

        public List<string> RoleNames { get; set; }

        public string IpAddress { get; set; }
    }
}
