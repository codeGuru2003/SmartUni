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
    public class ReligionTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReligionTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReligionTypes
        public async Task<IActionResult> Index()
        {
              return _context.ReligionTypes != null ? 
                          View(await _context.ReligionTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ReligionTypes'  is null.");
        }

        // GET: ReligionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReligionTypes == null)
            {
                return NotFound();
            }

            var religionType = await _context.ReligionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (religionType == null)
            {
                return NotFound();
            }

            return View(religionType);
        }

        // GET: ReligionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReligionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] ReligionType religionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(religionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(religionType);
        }

        // GET: ReligionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReligionTypes == null)
            {
                return NotFound();
            }

            var religionType = await _context.ReligionTypes.FindAsync(id);
            if (religionType == null)
            {
                return NotFound();
            }
            return View(religionType);
        }

        // POST: ReligionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] ReligionType religionType)
        {
            if (id != religionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(religionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReligionTypeExists(religionType.Id))
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
            return View(religionType);
        }

        // GET: ReligionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReligionTypes == null)
            {
                return NotFound();
            }

            var religionType = await _context.ReligionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (religionType == null)
            {
                return NotFound();
            }

            return View(religionType);
        }

        // POST: ReligionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReligionTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReligionTypes'  is null.");
            }
            var religionType = await _context.ReligionTypes.FindAsync(id);
            if (religionType != null)
            {
                _context.ReligionTypes.Remove(religionType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReligionTypeExists(int id)
        {
          return (_context.ReligionTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
