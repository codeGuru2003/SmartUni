using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;
using System.Security.Claims;

namespace SmartUni.Controllers
{
    public class CoursePlanningController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoursePlanningController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index(string? keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                var student = _context.Students.FirstOrDefault(x => x.StudentId.Equals(Convert.ToInt64(keyword)));
                return View(student);
            }
            return View();
        }

        public IActionResult StudentPlanning(int id)
        {
            var semesterId = HttpContext.Session.GetInt32("A_AsemesterId");
            var studentPlans = _context.StudentSections.Where(x => x.Id == id).Where(x => x.AcademicSemesterId == semesterId).ToList();
            return View(studentPlans);
        }
        [HttpGet]
        public async Task<IActionResult> CreateStudentSection(int id)
        {

            var semesterId = HttpContext.Session.GetInt32("A_AsemesterId");
            var sections = await _context.Sections.Where(x => x.AcademicSemesterId == semesterId).ToListAsync();
            ViewData["sections"] = new SelectList(sections, "Id", "Name");
            ViewData["sections"] = new SelectList(sections, "Id", "Name");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentSection(StudentSections studentSection)
        {
            if (ModelState.IsValid)
            {
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var student = await _context.Students.Where(x => x.UserID == userID).FirstOrDefaultAsync();

                if (student != null)
                {
                }


                _context.StudentSections.Add(studentSection);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
