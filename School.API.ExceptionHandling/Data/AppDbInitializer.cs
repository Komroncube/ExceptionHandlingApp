namespace School.API.ExceptionHandling.Data;

public class AppDbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
        context.Database.EnsureCreated();

        if (context.Students.Any())
        {
            return;
        }

        var students = new List<Student>
        {
            new Student
            {
                Name = "Harry Potter",
                DateOfBirth = new DateTime(1990, 1, 1)
            },
            new Student
            {
                Name = "Jane Doe",
                DateOfBirth = new DateTime(1995, 1, 1)
            }
        };

        context.Students.AddRange(students);
        context.SaveChanges();
    }
}
