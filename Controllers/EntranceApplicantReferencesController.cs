using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	public class EntranceApplicantReferencesController : Controller
	{
		private readonly ApplicationDbContext _context;
        public EntranceApplicantReferencesController(ApplicationDbContext context)
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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult >Create(EntranceApplicantReference reference)
		{
			if (ModelState.IsValid)
			{
				var statusId = await _context.StatusTypes.Where(s => s.Name.Contains("Pending")).FirstOrDefaultAsync();
				reference.StatusTypeID = statusId.Id;
				_context.EntranceApplicantReferences.Add(reference);
				await _context.SaveChangesAsync();
				return RedirectToAction("References","Admission");
			}
			return View();
		}
	}
}
