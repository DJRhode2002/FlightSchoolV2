using FlightSchoolV2.Data;
using FlightSchoolV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSchoolV2.Controllers
{
    [Authorize(Roles = "Admin,CFI")]
    public class PackagesAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PackagesAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PackagesAdmin
        public async Task<IActionResult> Index(int? packageId)
        {
            // If a packageId was passed, find it so we can pre-select it in the form
            if (packageId.HasValue)
            {
                var package = await _context.TrainingPackages.FindAsync(packageId);
                ViewBag.SelectedPackage = package?.Title;
            }
            return View();
        }

        // GET: PackagesAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingPackage = await _context.TrainingPackages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingPackage == null)
            {
                return NotFound();
            }

            return View(trainingPackage);
        }

        // GET: PackagesAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PackagesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,ImageUrl")] TrainingPackage trainingPackage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingPackage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingPackage);
        }

        // GET: PackagesAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingPackage = await _context.TrainingPackages.FindAsync(id);
            if (trainingPackage == null)
            {
                return NotFound();
            }
            return View(trainingPackage);
        }

        // POST: PackagesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,ImageUrl")] TrainingPackage trainingPackage)
        {
            if (id != trainingPackage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingPackage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingPackageExists(trainingPackage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trainingPackage);
        }

        // GET: PackagesAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingPackage = await _context.TrainingPackages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingPackage == null)
            {
                return NotFound();
            }

            return View(trainingPackage);
        }

        // POST: PackagesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingPackage = await _context.TrainingPackages.FindAsync(id);
            if (trainingPackage != null)
            {
                _context.TrainingPackages.Remove(trainingPackage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingPackageExists(int id)
        {
            return _context.TrainingPackages.Any(e => e.Id == id);
        }
    }
}
