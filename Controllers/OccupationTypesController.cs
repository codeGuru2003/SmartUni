using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class OccupationTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OccupationTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OccupationTypes
        public async Task<IActionResult> Index()
        {
              return _context.OccupationTypes != null ? 
                          View(await _context.OccupationTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.OccupationTypes'  is null.");
        }

        // GET: OccupationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OccupationTypes == null)
            {
                return NotFound();
            }

            var occupationType = await _context.OccupationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (occupationType == null)
            {
                return NotFound();
            }

            return View(occupationType);
        }

        // GET: OccupationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OccupationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] OccupationType occupationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(occupationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(occupationType);
        }

        // GET: OccupationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OccupationTypes == null)
            {
                return NotFound();
            }

            var occupationType = await _context.OccupationTypes.FindAsync(id);
            if (occupationType == null)
            {
                return NotFound();
            }
            return View(occupationType);
        }

        // POST: OccupationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] OccupationType occupationType)
        {
            if (id != occupationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(occupationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccupationTypeExists(occupationType.Id))
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
            return View(occupationType);
        }

        // GET: OccupationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OccupationTypes == null)
            {
                return NotFound();
            }

            var occupationType = await _context.OccupationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (occupationType == null)
            {
                return NotFound();
            }

            return View(occupationType);
        }

        // POST: OccupationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OccupationTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OccupationTypes'  is null.");
            }
            var occupationType = await _context.OccupationTypes.FindAsync(id);
            if (occupationType != null)
            {
                _context.OccupationTypes.Remove(occupationType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OccupationTypeExists(int id)
        {
          return (_context.OccupationTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
