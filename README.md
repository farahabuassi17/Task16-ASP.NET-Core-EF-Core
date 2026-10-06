# CampusFlow

CampusFlow is an ASP.NET Core Razor Pages web application developed to manage students and courses. The project demonstrates CRUD operations, Entity Framework Core, SQL Server, Repository Pattern, asynchronous programming, file uploading, search and filtering, and automated email notifications.

## Features

### Course Management
- Create new courses
- View course details
- Edit existing courses
- Delete courses
- Search courses by name
- Filter courses by credit hours
- Success and failure alert messages

### Student Management
- Create new students
- View student details
- Edit student information
- Delete students
- Search students by name or email
- Filter students by course
- Assign students to courses

### File Upload
- Upload a profile image when creating a student
- Store uploaded images inside `wwwroot/uploads/students`
- Display student images in the Students list
- Display student images on the Details page
- Change the student image from the Edit page
- Keep the current image if no new image is selected

### Email Service
CampusFlow includes an automated email notification service.

When a new student is created successfully, the system sends a welcome email to the student's email address.

Email credentials are stored securely using .NET User Secrets and are not included in the source code.

### Repository Pattern
The application uses the Repository Pattern to separate the data access layer from the Razor Pages.

Repositories:

- `ICourseRepository`
- `CourseRepository`
- `IStudentRepository`
- `StudentRepository`

This keeps database operations separate from the user interface logic.

### Async Operations
Database and service operations use `async` and `await`, including:

- Retrieving records
- Searching and filtering
- Creating records
- Updating records
- Deleting records
- Uploading files
- Sending emails

## Technologies Used

- C#
- ASP.NET Core Razor Pages
- .NET
- Entity Framework Core
- SQL Server
- LINQ
- Bootstrap
- HTML
- CSS
- Repository Pattern
- SMTP Email Service
- Git & GitHub

## Database

The application uses SQL Server with Entity Framework Core.

Main entities:

### Course

- CourseId
- Name
- CreditHours
- Students

### Student

- StudentId
- Name
- Email
- CourseId
- Course
- ImagePath

Relationship:

```text
Course
  |
  | 1
  |
  |------< Students
            Many
```

One course can have multiple students, while each student belongs to one course.

## Project Structure

```text
Task16WebApp
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Models
│   ├── Course.cs
│   └── Student.cs
│
├── Repositories
│   ├── ICourseRepository.cs
│   ├── CourseRepository.cs
│   ├── IStudentRepository.cs
│   └── StudentRepository.cs
│
├── Services
│   ├── IEmailService.cs
│   └── EmailService.cs
│
├── Pages
│   ├── Courses
│   └── Students
│
├── wwwroot
│   └── uploads
│       └── students
│
├── Program.cs
└── appsettings.json
```

## Database Configuration

Update the connection string in `appsettings.json` according to your SQL Server configuration.

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=Task16Database;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Then apply the migrations:

```bash
dotnet ef database update
```

## Email Configuration

The project uses .NET User Secrets to protect email credentials.

Initialize User Secrets:

```bash
dotnet user-secrets init
```

Configure Gmail SMTP:

```bash
dotnet user-secrets set "EmailSettings:Host" "smtp.gmail.com"
dotnet user-secrets set "EmailSettings:Port" "587"
dotnet user-secrets set "EmailSettings:Username" "YOUR_EMAIL@gmail.com"
dotnet user-secrets set "EmailSettings:Password" "YOUR_APP_PASSWORD"
```

A Gmail App Password should be used instead of storing the normal Gmail password in the project.

## Run the Project

Clone the repository:

```bash
git clone https://github.com/farahabuassi17/Task16-ASP.NET-Core-EF-Core.git
```

Navigate to the project:

```bash
cd Task16-ASP.NET-Core-EF-Core
```

Restore dependencies:

```bash
dotnet restore
```

Update the database:

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```

Then open the local URL displayed in the terminal.

## Task Requirements Completed

- Two related entities
- Full CRUD operations
- Search and filtering
- Success and failure alerts
- Repository layer
- Async/await operations
- File upload
- Automated email notification service
- Entity Framework Core
- SQL Server database
- GitHub repository

## Author

**Farah Abuassi**

Computer Systems Engineering Student
