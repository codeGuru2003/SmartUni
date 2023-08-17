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
    public class RelationshipTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelationshipTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RelationshipTypes
        public async Task<IActionResult> Index()
        {
              return _context.RelationshipTypes != null ? 
                          View(await _context.RelationshipTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.RelationshipTypes'  is null.");
        }

        // GET: RelationshipTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RelationshipTypes == null)
            {
                return NotFound();
            }

            var relationshipType = await _context.RelationshipTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationshipType == null)
            {
                return NotFound();
            }

            return View(relationshipType);
        }

        // GET: RelationshipTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RelationshipTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] RelationshipType relationshipType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relationshipType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relationshipType);
        }

        // GET: RelationshipTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RelationshipTypes == null)
            {
                return NotFound();
            }

            var relationshipType = await _context.RelationshipTypes.FindAsync(id);
            if (relationshipType == null)
            {
                return NotFound();
            }
            return View(relationshipType);
        }

        // POST: RelationshipTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] RelationshipType relationshipType)
        {
            if (id != relationshipType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relationshipType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelationshipTypeExists(relationshipType.Id))
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
            return View(relationshipType);
        }

        // GET: RelationshipTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RelationshipTypes == null)
            {
                return NotFound();
            }

            var relationshipType = await _context.RelationshipTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationshipType == null)
            {
                return NotFound();
            }

            return View(relationshipType);
        }

        // POST: RelationshipTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RelationshipTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RelationshipTypes'  is null.");
            }
            var relationshipType = await _context.RelationshipTypes.FindAsync(id);
            if (relationshipType != null)
            {
                _context.RelationshipTypes.Remove(relationshipType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelationshipTypeExists(int id)
        {
          return (_context.RelationshipTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
