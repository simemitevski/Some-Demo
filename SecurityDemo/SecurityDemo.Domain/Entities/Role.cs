using System.Collections.Generic;

namespace SecurityDemo.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
