using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task16WebApp.Models;
using Task16WebApp.Repositories;

namespace Task16WebApp.Pages.Courses;

public class DetailsModel : PageModel
{
    private readonly ICourseRepository _courseRepository;

    public DetailsModel(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public Course Course { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        Course = course;

        return Page();
    }
}