using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task16WebApp.Models;
using Task16WebApp.Repositories;

namespace Task16WebApp.Pages.Courses;

public class CreateModel : PageModel
{
    private readonly ICourseRepository _courseRepository;

    public CreateModel(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [BindProperty]
    public Course Course { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _courseRepository.AddAsync(Course);

        TempData["SuccessMessage"] =
            "Course created successfully!";

        return RedirectToPage("Index");
    }
}