using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	public class HarmonizeController : Controller
	{
		private readonly string stringkeyOne = "L$2jZ@8kR#9wQx5VfP!7T*c";
		private readonly string stringkeyTwo = "P#9kDvF@3C7qA!G2X$E5WzR";
		private readonly ApplicationDbContext _context;
		public HarmonizeController(ApplicationDbContext context)
        {
			_context = context;
        }
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(string key)
		{
			if (key == stringkeyOne || key == stringkeyTwo)
			{
                HttpContext.Session.SetString("HarmonizeKey", key);
                return RedirectToAction("Glorify");
			}
			return View();
		}

		public async Task<IActionResult> Glorify()
		{
            var session = HttpContext.Session.GetString("Token");
			if (string.IsNullOrEmpty(session))
			{
				return RedirectToAction("Login", "Account");
			}
            var harmony = await _context.Harmonize.Where(x => x.Id == 1).FirstOrDefaultAsync();
			return View(harmony);
		}
		[HttpPost]
		public async Task<IActionResult> Glorify(Harmonize model)
		{
			if (ModelState.IsValid)
			{
				_context.Harmonize.Update(model);
				await _context.SaveChangesAsync();
				HttpContext.Session.Remove("HarmonizeKey");
				return RedirectToAction("Login", "Account");
			}
			return View(model);
		}
	}
}
