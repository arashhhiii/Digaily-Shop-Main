using Microsoft.EntityFrameworkCore;
using Data.Class.entities.Order;
using Data.Class.entities.Account;

namespace Data.Class.Context
{
    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region Account
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        #endregion
        #region FilterData
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" });

            modelBuilder.Entity<User>()
                .HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .Property(user => user.RoleId)
                .HasDefaultValue(2);

            var cascadeFile = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach(var fk in cascadeFile)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            }
            modelBuilder.Entity<User>().
                HasQueryFilter(u => !u.isDelete);
        }
        #endregion

    }
}
