using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task16WebApp.Models;
using Task16WebApp.Repositories;

namespace Task16WebApp.Pages.Students;

public class DeleteModel : PageModel
{
    private readonly IStudentRepository _studentRepository;

    public DeleteModel(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync()
    {
        var student = await _studentRepository.GetByIdAsync(
            Student.StudentId);

        if (student == null)
        {
            return NotFound();
        }

        await _studentRepository.DeleteAsync(Student.StudentId);

        TempData["SuccessMessage"] =
            "Student deleted successfully!";

        return RedirectToPage("Index");
    }
}