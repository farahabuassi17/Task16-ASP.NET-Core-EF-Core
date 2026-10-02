namespace Task16WebApp.Models;

public class Course
{
    public int CourseId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CreditHours { get; set; }

    public List<Student> Students { get; set; } = new();
}