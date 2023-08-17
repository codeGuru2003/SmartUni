using Microsoft.AspNetCore.Mvc;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	public class OfferingTypesController : Controller
	{
		public readonly ApplicationDbContext _context;
        public OfferingTypesController(ApplicationDbContext context)
        {
			_context = context;
        }
        public IActionResult Index()
		{
			var offeringtypes = _context.OfferingTypes.ToList();
			return View(offeringtypes);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(OfferingType model)
		{
			if (ModelState.IsValid)
			{
				_context.OfferingTypes.Add(model);
				await _context.SaveChangesAsync();
				TempData["RecordSavedMessage"] = "Record Created Successfully";
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
	}
}
