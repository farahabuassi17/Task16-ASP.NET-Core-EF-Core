using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task16WebApp.Models;
using Task16WebApp.Repositories;

namespace Task16WebApp.Pages.Courses;

public class DeleteModel : PageModel
{
    private readonly ICourseRepository _courseRepository;

    public DeleteModel(ICourseRepository courseRepository)
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
        try
        {
            var course = await _courseRepository.GetByIdAsync(
                Course.CourseId);

            if (course == null)
            {
                TempData["ErrorMessage"] =
                    "Course not found.";

                return RedirectToPage("Index");
            }

            await _courseRepository.DeleteAsync(
                Course.CourseId);

            TempData["SuccessMessage"] =
                "Course deleted successfully!";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] =
                "Unable to delete this course. It may have students assigned to it.";
        }

        return RedirectToPage("Index");
    }
}