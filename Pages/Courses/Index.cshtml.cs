using Microsoft.AspNetCore.Mvc.RazorPages;
using Task16WebApp.Models;
using Task16WebApp.Repositories;

namespace Task16WebApp.Pages.Courses;

public class IndexModel : PageModel
{
    private readonly ICourseRepository _courseRepository;

    public IndexModel(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public IList<Course> Courses { get; set; }
        = new List<Course>();

    public string? SearchTerm { get; set; }

    public int? CreditHours { get; set; }

    public async Task OnGetAsync(
        string? searchTerm,
        int? creditHours)
    {
        SearchTerm = searchTerm;
        CreditHours = creditHours;

        Courses = await _courseRepository.SearchAsync(
            searchTerm,
            creditHours);
    }
}