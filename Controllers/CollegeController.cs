using Microsoft.AspNetCore.Mvc;
using SmartUni.Data;

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
	}
}
