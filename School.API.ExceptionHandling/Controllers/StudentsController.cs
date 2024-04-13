using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.API.ExceptionHandling.Data;
using School.API.ExceptionHandling.Exceptions;
using School.API.ExceptionHandling.Exceptions.Filters;

namespace School.API.ExceptionHandling.Controllers;
[Route("api/[controller]")]
[ApiController]
[CustomExceptionFilter]
public class StudentsController : ControllerBase
{
    private readonly SchoolDbContext _context;

    public StudentsController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet("get-students")]
    [CustomExceptionFilter]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        throw new Exception("test exception");

        try
        {
            return await _context.Students.ToListAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("add-new-student")]
    public async Task<ActionResult<Student>> PostStudent([FromBody] Student student)
    {
        try
        {
            if (Regex.IsMatch(student.Name, @"^\d"))
            {
                throw new StudentNameException("Student name cannot start with a number", student.Name);
            }

            if (StudentIs20OrYounger(student.DateOfBirth))
            {
                throw new StudentAgeException("Student must be older than 20 years old");
            }

            //throw new Exception("test exception");
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Created("", student);
        }
        catch (StudentNameException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (StudentAgeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    private bool StudentIs20OrYounger(DateTime studentDateOfBirth)
    {
        return DateTime.Now.Year - studentDateOfBirth.Year <= 20;
        
    }
}
