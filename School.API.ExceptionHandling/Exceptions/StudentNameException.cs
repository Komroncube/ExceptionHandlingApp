namespace School.API.ExceptionHandling.Exceptions;

public class StudentNameException : Exception
{
    public StudentNameException() : base()
    {
        
    }

    public StudentNameException(string message) : base(message)
    {
        
    }

    public StudentNameException(string message, Exception innerException) : base(message, innerException)
    {
        
    }

    public string StudentName { get; set; }

    public StudentNameException(string message, string studentName): base(message)
    {
        StudentName = studentName;
    }
}
