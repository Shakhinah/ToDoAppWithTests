using Microsoft.EntityFrameworkCore;
using ToDoApp.Core.Entities;

namespace ToDoApp.Infrastructure.Data
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; } // DbSet for ToDoItem

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the ToDoItem entity
            modelBuilder.Entity<ToDoItem>(entity =>
            {
                entity.HasKey(e => e.Id); // Set Id as primary key
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // Auto-increment
                entity.Property(e => e.Content).IsRequired(); // Content is required
                entity.Property(e => e.IsCompleted).HasDefaultValue(false); // Default value
            });
        }
    }
}