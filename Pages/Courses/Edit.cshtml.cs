using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task16WebApp.Models;
using Task16WebApp.Repositories;

namespace Task16WebApp.Pages.Courses;

public class EditModel : PageModel
{
    private readonly ICourseRepository _courseRepository;

    public EditModel(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _courseRepository.UpdateAsync(Course);

        TempData["SuccessMessage"] =
            "Course updated successfully!";

        return RedirectToPage("Index");
    }
}