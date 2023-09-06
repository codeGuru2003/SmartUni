using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class SectionTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SectionTypesController(ApplicationDbContext context)
        {
                _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sectionTypes = await _context.SectionTypes.ToListAsync();
            return View(sectionTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SectionType sectionType)
        {
            if (ModelState.IsValid)
            {
                _context.SectionTypes.Add(sectionType);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Record created successfully";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Error creating record";
            return View();
        }


        // GET: SectionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SectionTypes == null)
            {
                return NotFound();
            }

            var sectionType = await _context.SectionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectionType == null)
            {
                return NotFound();
            }

            return View(sectionType);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SectionTypes == null)
            {
                return NotFound();
            }

            var sectionType = await _context.SectionTypes.FindAsync(id);
            if (sectionType == null)
            {
                return NotFound();
            }
            return View(sectionType);
        }

        // POST: SectionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SectionType sectionType)
        {
            if (id != sectionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionTypeExist(sectionType.Id))
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
            return View(sectionType);
        }

        // GET: AcademicYearTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SectionTypes == null)
            {
                return NotFound();
            }

            var sectiontype = await _context.SectionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectiontype == null)
            {
                return NotFound();
            }

            return View(sectiontype);
        }

        // POST: AcademicYearTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SectionTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AcademicYearTypes'  is null.");
            }
            var sectiontype = await _context.SectionTypes.FindAsync(id);
            if (sectiontype != null)
            {
                _context.SectionTypes.Remove(sectiontype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool SectionTypeExist(int id)
        {
            return (_context.SectionTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
