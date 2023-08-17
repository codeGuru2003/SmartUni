using Microsoft.AspNetCore.Mvc;
using SmartUni.Data;

namespace SmartUni.Controllers
{
	public class CollegeType : Controller
	{
		private readonly ApplicationDbContext _context;
        public CollegeType(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			var collegetypes = _context.CollegeTypes.ToList();
			return View();
		}
	}
}
