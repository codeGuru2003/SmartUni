using Microsoft.AspNetCore.Mvc;
using SmartUni.Data;

namespace SmartUni.Controllers
{
	public class CollegeTypeController : Controller
	{
		private readonly ApplicationDbContext _context;
        public CollegeTypeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			var collegetypes = _context.CollegeTypes.ToList();
			return View();
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
	}
}
