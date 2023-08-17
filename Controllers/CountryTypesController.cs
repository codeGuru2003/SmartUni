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
    public class CountryTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountryTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CountryTypes
        public async Task<IActionResult> Index()
        {
              return _context.CountryTypes != null ? 
                          View(await _context.CountryTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CountryTypes'  is null.");
        }

        // GET: CountryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CountryTypes == null)
            {
                return NotFound();
            }

            var countryType = await _context.CountryTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryType == null)
            {
                return NotFound();
            }

            return View(countryType);
        }

        // GET: CountryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CountryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] CountryType countryType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(countryType);
        }

        // GET: CountryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CountryTypes == null)
            {
                return NotFound();
            }

            var countryType = await _context.CountryTypes.FindAsync(id);
            if (countryType == null)
            {
                return NotFound();
            }
            return View(countryType);
        }

        // POST: CountryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] CountryType countryType)
        {
            if (id != countryType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryTypeExists(countryType.Id))
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
            return View(countryType);
        }

        // GET: CountryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CountryTypes == null)
            {
                return NotFound();
            }

            var countryType = await _context.CountryTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryType == null)
            {
                return NotFound();
            }

            return View(countryType);
        }

        // POST: CountryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CountryTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CountryTypes'  is null.");
            }
            var countryType = await _context.CountryTypes.FindAsync(id);
            if (countryType != null)
            {
                _context.CountryTypes.Remove(countryType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryTypeExists(int id)
        {
          return (_context.CountryTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
