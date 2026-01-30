using Microsoft.AspNetCore.Mvc;
using FlightSchoolV2.Data;
using FlightSchoolV2.Models;
using FlightSchoolV2.Services;

public class RegistrationController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _host;
    private readonly IEmailService _email;

    public RegistrationController(ApplicationDbContext context, IWebHostEnvironment host, IEmailService email)
    {
        _context = context; _host = host; _email = email;
    }

    [HttpPost]
    public async Task<IActionResult> Register(Student student, IFormFile medicalFile)
    {
        if (ModelState.IsValid)
        {
            // 1. Save to DB
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // 2. Upload File
            if (medicalFile != null)
            {
                string path = Path.Combine(_host.WebRootPath, "Uploads", student.Id.ToString());
                Directory.CreateDirectory(path);
                string fullPath = Path.Combine(path, medicalFile.FileName);
                using (var s = new FileStream(fullPath, FileMode.Create)) { await medicalFile.CopyToAsync(s); }

                _context.Documents.Add(new StudentDocument
                {
                    StudentId = student.Id,
                    DocumentType = "Medical",
                    FilePath = "/Uploads/" + student.Id + "/" + medicalFile.FileName
                });
                await _context.SaveChangesAsync();
            }

            // 3. Email Admin
            await _email.SendEmailAsync("yamithethinker@gmail.com", "New Registration", $"New student {student.Name} applied for {student.SelectedPackage}.");

            return RedirectToAction("Success");
        }
        return View(student);
    }
}