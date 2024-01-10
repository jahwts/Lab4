using Lab4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.DB
{
    public sealed class LR4Context : DbContext
    {
        public DbSet<MyTask> Tasks { get; set; }

        public LR4Context(DbContextOptions<LR4Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyTask>().HasKey(x => x.Id);
        }
    }
}