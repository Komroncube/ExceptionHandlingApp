using Microsoft.EntityFrameworkCore;

namespace School.API.ExceptionHandling.Data;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                Name = "Marc Sucerbert",
                DateOfBirth = new DateTime(2000, 1, 1)
            },
            new Student
            {
                Id = 2,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2000, 3, 4)
            }
            );

    }


    public DbSet<Student> Students { get; set; }
}


