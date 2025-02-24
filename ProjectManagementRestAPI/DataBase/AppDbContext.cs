using Microsoft.EntityFrameworkCore;
using ProjectManagementRestAPI.Model;

namespace ProjectManagementRestAPI.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<StatusTask> StatusTasks { get; set; }
        public DbSet<Model.Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersProject> UsersProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Переопределяем SaveChanges() для автоматического обновления DateChange
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.DateChange = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreate = DateTime.UtcNow;
                    entry.Entity.DateChange = DateTime.UtcNow;
                }
            }
        }


    }
}
