using FlightSchoolV2.Models;
using Microsoft.AspNetCore.Mvc;
using FlightSchoolV2.Data; // Ensure this matches your namespac
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FlightSchoolV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        // 2. Add the constructor to "Inject" the database
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Packages()
        {
            var packages = await _context.TrainingPackages.ToListAsync();
            return View(packages);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
