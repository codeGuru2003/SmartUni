using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class FacultyTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FacultyTypeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var facultyTypes = await _context.FacultyTypes.ToListAsync();
            return View(facultyTypes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FacultyTypes == null)
            {
                return NotFound();
            }

            var facultyType = await _context.FacultyTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyType == null)
            {
                return NotFound();
            }

            return View(facultyType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] FacultyType facultyType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultyType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facultyType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FacultyTypes == null)
            {
                return NotFound();
            }

            var facultyTypes = await _context.FacultyTypes.FindAsync(id);
            if (facultyTypes == null)
            {
                return NotFound();
            }
            return View(facultyTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] FacultyType facultyType)
        {
            if (id != facultyType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultyType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyTypeExists(facultyType.Id))
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
            return View(facultyType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FacultyTypes == null)
            {
                return NotFound();
            }

            var facultyType = await _context.FacultyTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyType == null)
            {
                return NotFound();
            }

            return View(facultyType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Genders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Genders'  is null.");
            }
            var facultyType = await _context.FacultyTypes.FindAsync(id);
            if (facultyType != null)
            {
                _context.FacultyTypes.Remove(facultyType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyTypeExists(int id)
        {
            return (_context.FacultyTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
