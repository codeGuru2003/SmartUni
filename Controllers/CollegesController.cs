using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	public class CollegesController : Controller
	{
		private readonly ApplicationDbContext _context;
        public CollegesController(ApplicationDbContext context)
        {
			_context = context;
        }
		[HttpGet]
        public IActionResult Index()
		{
			var colleges = _context.College.ToList();
			return View(colleges);
		}
        // GET: AcademicYearTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.College == null)
            {
                return NotFound();
            }

            var college = await _context.College.Include(x=>x.CollegeType).FirstOrDefaultAsync(m => m.Id == id);
            if (college == null)
            {
                return NotFound();
            }

            return View(college);
        }

        [HttpGet]
		public IActionResult Create()
		{
			ViewData["CollegeTypeId"] = new SelectList(_context.CollegeTypes, "Id", "Name");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(College college)
		{
            if (college.CollegeTypeID <= 0)
            {
                TempData["Message"] = "College Type Id is null";
                return Redirect("Index");
            }
            else
            {
                _context.College.Add(college);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Record created successfully";
                return Redirect("Index");
            }
		}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.College == null)
            {
                return NotFound();
            }

            var college = await _context.College.FindAsync(id);
            if (college == null)
            {
                return NotFound();
            }
            ViewData["CollegeTypeId"] = new SelectList(_context.CollegeTypes, "Id", "Name", college.CollegeTypeID);
            return View(college);
        }

        // POST: AcademicYearTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] College college)
        {
            if (id != college.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(college);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollegeExists(college.Id))
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
            ViewData["CollegeTypeId"] = new SelectList(_context.CollegeTypes, "Id", "Name",college.CollegeTypeID);
            return View(college);
        }

        private bool CollegeExists(int id)
        {
            return (_context.College?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
