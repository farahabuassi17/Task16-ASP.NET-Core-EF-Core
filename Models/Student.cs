namespace Task16WebApp.Models;

public class Student
{
    public int StudentId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int CourseId { get; set; }

    public Course Course { get; set; } = null!;

    public string? ImagePath { get; set; }
}