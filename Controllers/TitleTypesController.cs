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
    public class TitleTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TitleTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TitleTypes
        public async Task<IActionResult> Index()
        {
              return _context.TitleTypes != null ? 
                          View(await _context.TitleTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TitleTypes'  is null.");
        }

        // GET: TitleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TitleTypes == null)
            {
                return NotFound();
            }

            var titleType = await _context.TitleTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (titleType == null)
            {
                return NotFound();
            }

            return View(titleType);
        }

        // GET: TitleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TitleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] TitleType titleType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(titleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(titleType);
        }

        // GET: TitleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TitleTypes == null)
            {
                return NotFound();
            }

            var titleType = await _context.TitleTypes.FindAsync(id);
            if (titleType == null)
            {
                return NotFound();
            }
            return View(titleType);
        }

        // POST: TitleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] TitleType titleType)
        {
            if (id != titleType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(titleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TitleTypeExists(titleType.Id))
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
            return View(titleType);
        }

        // GET: TitleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TitleTypes == null)
            {
                return NotFound();
            }

            var titleType = await _context.TitleTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (titleType == null)
            {
                return NotFound();
            }

            return View(titleType);
        }

        // POST: TitleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TitleTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TitleTypes'  is null.");
            }
            var titleType = await _context.TitleTypes.FindAsync(id);
            if (titleType != null)
            {
                _context.TitleTypes.Remove(titleType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TitleTypeExists(int id)
        {
          return (_context.TitleTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
