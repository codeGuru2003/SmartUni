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
    public class SemesterTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SemesterTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SemesterTypes
        public async Task<IActionResult> Index()
        {
              return _context.SemesterTypes != null ? 
                          View(await _context.SemesterTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SemesterTypes'  is null.");
        }

        // GET: SemesterTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SemesterTypes == null)
            {
                return NotFound();
            }

            var semesterType = await _context.SemesterTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (semesterType == null)
            {
                return NotFound();
            }

            return View(semesterType);
        }

        // GET: SemesterTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SemesterTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] SemesterType semesterType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semesterType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semesterType);
        }

        // GET: SemesterTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SemesterTypes == null)
            {
                return NotFound();
            }

            var semesterType = await _context.SemesterTypes.FindAsync(id);
            if (semesterType == null)
            {
                return NotFound();
            }
            return View(semesterType);
        }

        // POST: SemesterTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] SemesterType semesterType)
        {
            if (id != semesterType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semesterType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemesterTypeExists(semesterType.Id))
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
            return View(semesterType);
        }

        // GET: SemesterTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SemesterTypes == null)
            {
                return NotFound();
            }

            var semesterType = await _context.SemesterTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (semesterType == null)
            {
                return NotFound();
            }

            return View(semesterType);
        }

        // POST: SemesterTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SemesterTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SemesterTypes'  is null.");
            }
            var semesterType = await _context.SemesterTypes.FindAsync(id);
            if (semesterType != null)
            {
                _context.SemesterTypes.Remove(semesterType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SemesterTypeExists(int id)
        {
          return (_context.SemesterTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
