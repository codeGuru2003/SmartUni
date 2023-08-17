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
    public class GenderTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenderTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GenderTypes
        public async Task<IActionResult> Index()
        {
              return _context.Genders != null ? 
                          View(await _context.Genders.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Genders'  is null.");
        }

        // GET: GenderTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Genders == null)
            {
                return NotFound();
            }

            var genderType = await _context.Genders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genderType == null)
            {
                return NotFound();
            }

            return View(genderType);
        }

        // GET: GenderTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GenderTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] GenderType genderType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genderType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genderType);
        }

        // GET: GenderTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Genders == null)
            {
                return NotFound();
            }

            var genderType = await _context.Genders.FindAsync(id);
            if (genderType == null)
            {
                return NotFound();
            }
            return View(genderType);
        }

        // POST: GenderTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] GenderType genderType)
        {
            if (id != genderType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genderType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderTypeExists(genderType.Id))
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
            return View(genderType);
        }

        // GET: GenderTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Genders == null)
            {
                return NotFound();
            }

            var genderType = await _context.Genders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genderType == null)
            {
                return NotFound();
            }

            return View(genderType);
        }

        // POST: GenderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Genders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Genders'  is null.");
            }
            var genderType = await _context.Genders.FindAsync(id);
            if (genderType != null)
            {
                _context.Genders.Remove(genderType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenderTypeExists(int id)
        {
          return (_context.Genders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
