using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;
using SmartUni.ViewModels;

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
		public async Task<IActionResult> Index()
		{
			var students = await _context.Students.ToListAsync();
			return View(students);
		}
		public IActionResult Create()
		{
			ViewData["TitleTypeId"] = new SelectList(_context.TitleTypes, "Id", "Name");
			ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
			ViewData["NationalityId"] = new SelectList(_context.NationalityTypes, "Id", "Name");
			ViewData["CountryId"] = new SelectList(_context.CountryTypes, "Id", "Name");
			ViewData["ReligionId"] = new SelectList(_context.ReligionTypes, "Id", "Name");
			ViewData["MaritalStatusId"] = new SelectList(_context.MaritalStatusTypes, "Id", "Name");
			ViewData["OccupationId"] = new SelectList(_context.OccupationTypes, "Id", "Name");
			ViewData["RelationshipId"] = new SelectList(_context.RelationshipTypes, "Id", "Name");
			ViewData["DisabilityId"] = new SelectList(_context.DisabilityTypes, "Id", "Name");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Student student)
		{
			return View();
		}

		long generateStudentId(Student lastStudentId)
		{
			if (lastStudentId == null)
			{
				return (0001);
			}
			return lastStudentId.StudentId + 1;

		}

		public IActionResult ManagePlanning(string? keyword)
		{
			if (!String.IsNullOrEmpty(keyword))
			{
                var student = _context.Students.FirstOrDefault(x => x.StudentId.Equals(Convert.ToInt64(keyword)));
                return View(student);
            }
            return View();
        }
	}
}
