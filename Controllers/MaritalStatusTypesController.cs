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
    public class MaritalStatusTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaritalStatusTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MaritalStatusTypes
        public async Task<IActionResult> Index()
        {
              return _context.MaritalStatusTypes != null ? 
                          View(await _context.MaritalStatusTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MaritalStatusTypes'  is null.");
        }

        // GET: MaritalStatusTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MaritalStatusTypes == null)
            {
                return NotFound();
            }

            var maritalStatusType = await _context.MaritalStatusTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maritalStatusType == null)
            {
                return NotFound();
            }

            return View(maritalStatusType);
        }

        // GET: MaritalStatusTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaritalStatusTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] MaritalStatusType maritalStatusType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maritalStatusType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maritalStatusType);
        }

        // GET: MaritalStatusTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MaritalStatusTypes == null)
            {
                return NotFound();
            }

            var maritalStatusType = await _context.MaritalStatusTypes.FindAsync(id);
            if (maritalStatusType == null)
            {
                return NotFound();
            }
            return View(maritalStatusType);
        }

        // POST: MaritalStatusTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] MaritalStatusType maritalStatusType)
        {
            if (id != maritalStatusType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maritalStatusType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaritalStatusTypeExists(maritalStatusType.Id))
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
            return View(maritalStatusType);
        }

        // GET: MaritalStatusTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MaritalStatusTypes == null)
            {
                return NotFound();
            }

            var maritalStatusType = await _context.MaritalStatusTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maritalStatusType == null)
            {
                return NotFound();
            }

            return View(maritalStatusType);
        }

        // POST: MaritalStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MaritalStatusTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MaritalStatusTypes'  is null.");
            }
            var maritalStatusType = await _context.MaritalStatusTypes.FindAsync(id);
            if (maritalStatusType != null)
            {
                _context.MaritalStatusTypes.Remove(maritalStatusType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaritalStatusTypeExists(int id)
        {
          return (_context.MaritalStatusTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
