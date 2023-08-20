using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	public class CollegeTypesController : Controller
	{
		private readonly ApplicationDbContext _context;
        public CollegeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			var collegetypes = _context.CollegeTypes.ToList();
			return View(collegetypes);
		}

        // GET: DisabilityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CollegeTypes == null)
            {
                return NotFound();
            }

            var disabilityType = await _context.CollegeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disabilityType == null)
            {
                return NotFound();
            }

            return View(disabilityType);
        }

        [HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CollegeType collegetype)
		{
			if (ModelState.IsValid)
			{
				_context.CollegeTypes.Add(collegetype);
				await _context.SaveChangesAsync();
				TempData["Message"] = "Record created successfully";
				return Redirect("Index");
			}
            TempData["Message"] = "Error creating record";
            return View("Index");   
		}

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CollegeTypes == null)
            {
                return NotFound();
            }

            var relationshipType = await _context.CollegeTypes.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, CollegeType collegetype)
        {
            if (id != collegetype.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collegetype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollegeTypeExist(collegetype.Id))
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
            return View(collegetype);
        }

        // GET: AcademicYearTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CollegeTypes == null)
            {
                return NotFound();
            }

            var collegeType = await _context.CollegeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collegeType == null)
            {
                return NotFound();
            }

            return View(collegeType);
        }

        // POST: AcademicYearTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CollegeTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AcademicYearTypes'  is null.");
            }
            var collegeType = await _context.CollegeTypes.FindAsync(id);
            if (collegeType != null)
            {
                _context.CollegeTypes.Remove(collegeType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CollegeTypeExist(int id)
        {
            return (_context.CollegeTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
