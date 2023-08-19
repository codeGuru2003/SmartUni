using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	public class StudentController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		
		public StudentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) 
		{ 
			_context = context;
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			var students = _context.Students.ToListAsync();
			return View();
		}
	}

	
}
