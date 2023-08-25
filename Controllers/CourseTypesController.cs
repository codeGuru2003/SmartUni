using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	public class CourseTypesController : Controller
	{
		private readonly ApplicationDbContext _context;
        public CourseTypesController(ApplicationDbContext context)
        {
			_context = context;
        }
        public async Task<IActionResult> Index()
		{
			var coursetypes = await _context.CourseTypes.ToListAsync();
			return View(coursetypes);
		}
		public IActionResult Create()
		{
			return View();
		}
		public IActionResult Create(CourseType coursetype)
		{
			return View();
		}
	}
}
