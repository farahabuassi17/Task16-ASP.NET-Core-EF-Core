using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task16WebApp.Models;
using Task16WebApp.Repositories;

namespace Task16WebApp.Pages.Students;

public class DetailsModel : PageModel
{
    private readonly IStudentRepository _studentRepository;

    public DetailsModel(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public Student Student { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        Student = student;

        return Page();
    }
}