using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class CoursePlanning : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursePlanning(ApplicationDbContext context)
        {
            _context = context;
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

        public IActionResult StudentPlannig(int id)
        {
            var semesterId = HttpContext.Session.GetInt32("A_AsemesterId");
            var studentPlans = _context.StudentSections.Where(x => x.Id == id).Where(
                x => x.AcademicSemesterId == semesterId).ToList();

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
                _context.StudentSections.Add(studentSection);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
