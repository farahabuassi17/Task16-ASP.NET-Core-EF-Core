using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task16WebApp.Models;
using Task16WebApp.Repositories;
using Task16WebApp.Services;

namespace Task16WebApp.Pages.Students;

public class CreateModel : PageModel
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IWebHostEnvironment _environment;
    private readonly IEmailService _emailService;

    public CreateModel(
        IStudentRepository studentRepository,
        ICourseRepository courseRepository,
        IWebHostEnvironment environment,
        IEmailService emailService)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _environment = environment;
        _emailService = emailService;
    }

    [BindProperty]
    public Student Student { get; set; } = new();

    [BindProperty]
    public IFormFile? ImageFile { get; set; }

    public SelectList CourseList { get; set; } = null!;

    public async Task OnGetAsync()
    {
        await LoadCoursesAsync();
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
            // Upload student image
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

                Student.ImagePath =
                    "/uploads/students/" + uniqueFileName;
            }

            // Save student to database
            await _studentRepository.AddAsync(Student);

            // Send automatic email
            await _emailService.SendEmailAsync(
                Student.Email,
                "Welcome to CampusFlow",
                $"""
                <h2>Welcome to CampusFlow, {Student.Name}!</h2>

                <p>Your student record has been created successfully.</p>

                <p>
                    Thank you for using CampusFlow.
                </p>
                """
            );

            TempData["SuccessMessage"] =
                "Student created successfully and email sent!";

            return RedirectToPage("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] =
                "Student was created, but there was a problem sending the email.";

            return RedirectToPage("Index");
        }
    }

    private async Task LoadCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();

        CourseList = new SelectList(
            courses,
            "CourseId",
            "Name");
    }
}