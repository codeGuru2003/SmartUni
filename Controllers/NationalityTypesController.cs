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
    public class NationalityTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NationalityTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NationalityTypes
        public async Task<IActionResult> Index()
        {
              return _context.NationalityTypes != null ? 
                          View(await _context.NationalityTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.NationalityTypes'  is null.");
        }

        // GET: NationalityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NationalityTypes == null)
            {
                return NotFound();
            }

            var nationalityType = await _context.NationalityTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nationalityType == null)
            {
                return NotFound();
            }

            return View(nationalityType);
        }

        // GET: NationalityTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NationalityTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] NationalityType nationalityType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nationalityType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nationalityType);
        }

        // GET: NationalityTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NationalityTypes == null)
            {
                return NotFound();
            }

            var nationalityType = await _context.NationalityTypes.FindAsync(id);
            if (nationalityType == null)
            {
                return NotFound();
            }
            return View(nationalityType);
        }

        // POST: NationalityTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] NationalityType nationalityType)
        {
            if (id != nationalityType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nationalityType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NationalityTypeExists(nationalityType.Id))
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
            return View(nationalityType);
        }

        // GET: NationalityTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NationalityTypes == null)
            {
                return NotFound();
            }

            var nationalityType = await _context.NationalityTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nationalityType == null)
            {
                return NotFound();
            }

            return View(nationalityType);
        }

        // POST: NationalityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NationalityTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NationalityTypes'  is null.");
            }
            var nationalityType = await _context.NationalityTypes.FindAsync(id);
            if (nationalityType != null)
            {
                _context.NationalityTypes.Remove(nationalityType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NationalityTypeExists(int id)
        {
          return (_context.NationalityTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
