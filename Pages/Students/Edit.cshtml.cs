using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task16WebApp.Models;
using Task16WebApp.Repositories;

namespace Task16WebApp.Pages.Students;

public class EditModel : PageModel
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IWebHostEnvironment _environment;

    public EditModel(
        IStudentRepository studentRepository,
        ICourseRepository courseRepository,
        IWebHostEnvironment environment)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _environment = environment;
    }

    [BindProperty]
    public Student Student { get; set; } = null!;

    [BindProperty]
    public IFormFile? ImageFile { get; set; }

    public SelectList CourseList { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        Student = student;

        await LoadCoursesAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadCoursesAsync();
            return Page();
        }

        try
        {
            var existingStudent =
                await _studentRepository.GetByIdAsync(Student.StudentId);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = Student.Name;
            existingStudent.Email = Student.Email;
            existingStudent.CourseId = Student.CourseId;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "students");

                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(ImageFile.FileName);

                string filePath = Path.Combine(
                    uploadsFolder,
                    uniqueFileName);

                using (var fileStream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                existingStudent.ImagePath =
                    "/uploads/students/" + uniqueFileName;
            }

            await _studentRepository.UpdateAsync(existingStudent);

            TempData["SuccessMessage"] =
                "Student updated successfully!";

            return RedirectToPage("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] =
                "Unable to update the student. Please try again.";

            return RedirectToPage("Index");
        }
    }

    private async Task LoadCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();

        CourseList = new SelectList(
            courses,
            "CourseId",
            "Name",
            Student.CourseId);
    }
}