using Microsoft.EntityFrameworkCore;
using TodoListApp.Entities;

namespace TodoListApp.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("datetime('now', 'utc')");
        }
    }
}