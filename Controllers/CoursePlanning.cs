using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;
using System.Security.Claims;

namespace SmartUni.Controllers
{
    public class CoursePlanning : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoursePlanning(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                int ID = int.Parse(search);
                var student = _context.Students.Where(x => x.Id == ID).FirstOrDefault();

                if (student != null)
                {
                    return View(student);
                }
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
        public IActionResult CreateStudentSection()
        {
            ViewData["TitleTypeId"] = new SelectList(_context.TitleTypes, "Id", "Name");
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentSection(StudentSections studentSection)
        {
            if (ModelState.IsValid)
            {
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var student = await _context.Students.Where(x => x.Id ==studentSection.StudentId).FirstOrDefaultAsync();
                _context.StudentSections.Add(studentSection);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
