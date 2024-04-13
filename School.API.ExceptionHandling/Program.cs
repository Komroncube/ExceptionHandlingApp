using Microsoft.EntityFrameworkCore;
using School.API.ExceptionHandling.Data;
using School.API.ExceptionHandling.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SchoolDbContext>(options => options.UseInMemoryDatabase("SchoolDb"));

var app = builder.Build();

AppDbInitializer.Seed(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

//Exception handling middleware
//app.ConfigureBuiltInExceptionHandler();

app.ConfigureCustomExceptionHandler();

//app.ConfigureExceptionHandler();

app.MapControllers();

app.Run();
