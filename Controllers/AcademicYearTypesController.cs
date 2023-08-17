using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class AcademicYearTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AcademicYearTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AcademicYearTypes
        public async Task<IActionResult> Index()
        {
              return _context.AcademicYearTypes != null ? 
                          View(await _context.AcademicYearTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AcademicYearTypes'  is null.");
        }

        // GET: AcademicYearTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AcademicYearTypes == null)
            {
                return NotFound();
            }

            var academicYearType = await _context.AcademicYearTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicYearType == null)
            {
                return NotFound();
            }

            return View(academicYearType);
        }

        // GET: AcademicYearTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AcademicYearTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] AcademicYearType academicYearType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicYearType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(academicYearType);
        }

        // GET: AcademicYearTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AcademicYearTypes == null)
            {
                return NotFound();
            }

            var academicYearType = await _context.AcademicYearTypes.FindAsync(id);
            if (academicYearType == null)
            {
                return NotFound();
            }
            return View(academicYearType);
        }

        // POST: AcademicYearTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] AcademicYearType academicYearType)
        {
            if (id != academicYearType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicYearType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicYearTypeExists(academicYearType.Id))
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
            return View(academicYearType);
        }

        // GET: AcademicYearTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AcademicYearTypes == null)
            {
                return NotFound();
            }

            var academicYearType = await _context.AcademicYearTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicYearType == null)
            {
                return NotFound();
            }

            return View(academicYearType);
        }

        // POST: AcademicYearTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AcademicYearTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AcademicYearTypes'  is null.");
            }
            var academicYearType = await _context.AcademicYearTypes.FindAsync(id);
            if (academicYearType != null)
            {
                _context.AcademicYearTypes.Remove(academicYearType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicYearTypeExists(int id)
        {
          return (_context.AcademicYearTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
