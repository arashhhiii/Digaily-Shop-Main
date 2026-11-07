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
       // public DbSet<User> Users { get; set; }
        #endregion
        #region FilterData
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFile = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach(var fk in cascadeFile)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            }
            //modelBuilder.Entity<User>().
            //    HasQueryFilter(u => !u.isDelete);
        }
        #endregion

    }
}
