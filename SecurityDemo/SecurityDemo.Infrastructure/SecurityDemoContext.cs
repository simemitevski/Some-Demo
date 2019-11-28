using Microsoft.EntityFrameworkCore;
using SecurityDemo.Domain.Entities;

namespace SecurityDemo.Data
{
    public class SecurityDemoContext : DbContext, ISecurityDemoContext
    {
        public SecurityDemoContext()
        {
        }

        public SecurityDemoContext(DbContextOptions<SecurityDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserActivation> UserActivation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(250);

                //the realationships for this entity are configured in the configurations for other entities UserRole and UserActivation
            });

            modelBuilder.Entity<UserActivation>(entity => {
                entity.HasOne(e => e.User)
                      .WithMany(e => e.UserActivations)
                      .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<UserRole>(entity=> {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(e => e.User)
                      .WithMany(e => e.UserRoles)
                      .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Role)
                      .WithMany(e => e.UserRoles)
                      .HasForeignKey(e => e.RoleId);
            });
        }
    }
}
