using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
	}
}
