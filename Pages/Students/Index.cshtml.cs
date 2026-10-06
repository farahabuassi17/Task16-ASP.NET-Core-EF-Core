using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task16WebApp.Data;
using Task16WebApp.Models;

namespace Task16WebApp.Pages.Students;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Student> Students { get; set; } = new List<Student>();

    public SelectList CourseList { get; set; } = null!;

    public string? SearchTerm { get; set; }

    public int? CourseId { get; set; }

    public async Task OnGetAsync(string? searchTerm, int? courseId)
    {
        SearchTerm = searchTerm;
        CourseId = courseId;

        var query = _context.Students
            .Include(s => s.Course)
            .AsQueryable();

        // Search by student name or email
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(s =>
                s.Name.Contains(searchTerm) ||
                s.Email.Contains(searchTerm));
        }

        // Filter by course
        if (courseId.HasValue)
        {
            query = query.Where(s => s.CourseId == courseId.Value);
        }

        Students = await query
            .OrderBy(s => s.Name)
            .ToListAsync();

        var courses = await _context.Courses
            .OrderBy(c => c.Name)
            .ToListAsync();

        CourseList = new SelectList(
            courses,
            "CourseId",
            "Name",
            CourseId
        );
    }
}