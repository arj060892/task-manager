using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Entities;

namespace TaskManager.Data
{
    public class UserTaskManagerDbContext : DbContext
    {
        public UserTaskManagerDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Setting up constraints and configurations
            modelBuilder.Entity<UserTask>()
                .HasIndex(t => t.Title)
                .IsUnique();

            modelBuilder.Entity<UserTask>()
                .Property(t => t.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<UserTask>()
                .Property(t => t.ModifiedDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
