using Domain;
using Microsoft.EntityFrameworkCore;

namespace CourseDbContext;

public class LearningCourseDataBaseContext : DbContext
{
    public DbSet<User> User { get; set; }

    public DbSet<Course> Course { get; set; }

    public DbSet<Raiting> Raiting { get; set; }

    public DbSet<Subscription> Subscription { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>();
        modelBuilder.Entity<Course>();
        modelBuilder.Entity<Raiting>();
        modelBuilder.Entity<Subscription>();
    }
}
