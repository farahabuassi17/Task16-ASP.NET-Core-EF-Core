using Task16WebApp.Models;

namespace Task16WebApp.Repositories;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();

    Task<List<Course>> SearchAsync(
        string? searchTerm,
        int? creditHours);

    Task<Course?> GetByIdAsync(int id);

    Task AddAsync(Course course);

    Task UpdateAsync(Course course);

    Task DeleteAsync(int id);
}