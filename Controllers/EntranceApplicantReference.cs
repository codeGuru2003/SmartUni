using Humanizer;
using Microsoft.AspNetCore.Mvc;
using SmartUni.Data;

namespace SmartUni.Controllers
{
	public class EntranceApplicantReference : Controller
	{
		private readonly ApplicationDbContext _context;
        public EntranceApplicantReference(ApplicationDbContext context)
        {
				_context = context;
        }
        public IActionResult Index()
		{
			var references = _context.EntranceApplicantReferences.ToList();
			return View(references);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(EntranceApplicantReference reference)
		{
			return View();
		}
	}
}
