using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;

namespace SmartUni.Controllers
{
	public class CoursesController : Controller
	{
		private readonly ApplicationDbContext _context;
        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
		{
			var courses = await _context.Courses.ToListAsync();
			return View(courses);
		}
	}
}
