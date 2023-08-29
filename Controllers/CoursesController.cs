using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;
using System.Configuration;

namespace SmartUni.Controllers
{
	public class CoursesController : Controller
	{
		private readonly ApplicationDbContext _context;
        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
		{
			var courses = await _context.Courses.ToListAsync();
			return View(courses);
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewData["DepartmentID"] = new SelectList(_context.Departments, "Id", "Name");
			ViewData["CourseTypeID"] = new SelectList(_context.CourseTypes, "Id", "Name");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(Course course)
		{
			if (course != null)
			{
				course.IsActive = true != null;
				course.IsLab = true != null;
				_context.Courses.Add(course);
				await _context.SaveChangesAsync();
				TempData["Message"] = "Course was created successfully";
				return RedirectToAction("Index","Courses");
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Details(int Id)
		{
			var course = await _context.Courses.Where(x=>x.Id == Id)
				.Include(s=>s.Department)
				.Include(m=>m.CourseType)
				.FirstOrDefaultAsync();
			ViewData["DepartmentID"] = new SelectList(_context.Departments, "Id", "Name", course.DepartmentID);
			return View(course);
		}
	}
}
