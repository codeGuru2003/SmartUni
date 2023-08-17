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
    public class DisabilityTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisabilityTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DisabilityTypes
        public async Task<IActionResult> Index()
        {
              return _context.DisabilityTypes != null ? 
                          View(await _context.DisabilityTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DisabilityTypes'  is null.");
        }

        // GET: DisabilityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DisabilityTypes == null)
            {
                return NotFound();
            }

            var disabilityType = await _context.DisabilityTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disabilityType == null)
            {
                return NotFound();
            }

            return View(disabilityType);
        }

        // GET: DisabilityTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DisabilityTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] DisabilityType disabilityType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disabilityType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disabilityType);
        }

        // GET: DisabilityTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DisabilityTypes == null)
            {
                return NotFound();
            }

            var disabilityType = await _context.DisabilityTypes.FindAsync(id);
            if (disabilityType == null)
            {
                return NotFound();
            }
            return View(disabilityType);
        }

        // POST: DisabilityTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] DisabilityType disabilityType)
        {
            if (id != disabilityType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disabilityType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisabilityTypeExists(disabilityType.Id))
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
            return View(disabilityType);
        }

        // GET: DisabilityTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DisabilityTypes == null)
            {
                return NotFound();
            }

            var disabilityType = await _context.DisabilityTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disabilityType == null)
            {
                return NotFound();
            }

            return View(disabilityType);
        }

        // POST: DisabilityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DisabilityTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DisabilityTypes'  is null.");
            }
            var disabilityType = await _context.DisabilityTypes.FindAsync(id);
            if (disabilityType != null)
            {
                _context.DisabilityTypes.Remove(disabilityType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisabilityTypeExists(int id)
        {
          return (_context.DisabilityTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
