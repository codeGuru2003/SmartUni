using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	public class CollegeController : Controller
	{
		private readonly ApplicationDbContext _context;
        public CollegeController(ApplicationDbContext context)
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
		public IActionResult Create(College college)
		{
			if (ModelState.IsValid)
			{

			}
			return View();
		}
	}
}
