using Microsoft.AspNetCore.Mvc;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	public class BillingTypesController : Controller
	{
		private readonly ApplicationDbContext _context;
        public BillingTypesController(ApplicationDbContext context)
        {
			_context = context;
        }
		[HttpGet]
        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BillingType model)
		{
			if (ModelState.IsValid)
			{
				_context.BillingTypes.Add(model);
				await _context.SaveChangesAsync();
				TempData["Message"] = "Record created successfully";
				return RedirectToAction("Index");
			}
			TempData["Message"] = "Error creating record";
			return View();
		}

		[HttpGet]
		public IActionResult Details(int id)
		{
			var billingtype = _context.BillingTypes.SingleOrDefault(x => x.Id == id);
			if (billingtype != null)
			{
				return View(billingtype);
			}
			return View();
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var billingtype = _context.BillingTypes.SingleOrDefault(x => x.Id == id);
			if (billingtype != null)
			{
				return View(billingtype);
			}
			return View();
		}
	}
}
