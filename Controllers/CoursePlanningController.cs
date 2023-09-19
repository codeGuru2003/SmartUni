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
            var semesterId = _context.AcademicSemesters.SingleOrDefault(x => x.IsActive == true);
            var studentPlans = _context.StudentSections.Where(x => x.StudentId == id).Where(x => x.AcademicSemester == semesterId)
                .Include(x=>x.Sections)
                .Include(x=>x.Course)
                .Include(x=>x.StatusTypes)
                .Include(x=>x.Sections.Faculty)
                .ToList();
            ViewBag.Student = id;
            return View(studentPlans);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateStudentSection(StudentSections studentSection)
        {
            if (ModelState.IsValid)
            {
                if (studentSection.StudentId == null)
                {
                    TempData["Message"] = "Adding Section Failed";
                    return RedirectToAction("Index");
                }
                var section = _context.Sections.Single(x => x.Id == studentSection.SectionId);
                var semesterId = _context.AcademicSemesters.SingleOrDefault(x => x.IsActive == true);
                var status = _context.StatusTypes.Single(x => x.Name.Contains("Created"));
                studentSection.AcademicSemesterId = semesterId.Id;
                studentSection.StatusTypesId = status.Id;
                if (section.Occupant < section.Capacity)
                {
                    section.Occupant += 1;
                }
                _context.StudentSections.Add(studentSection);
                _context.Sections.Update(section);
                await _context.SaveChangesAsync();
                return RedirectToAction("StudentPlanning", new { id = studentSection.StudentId });
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> SubmitSection(int studentSectionId)
        {
            var studentSection = _context.StudentSections.Single(x=>x.Id ==  studentSectionId);
            if (studentSection == null)
            {
                TempData["Message"] = "Error submitting session";
                return RedirectToAction("StudentPlanning", new { id = studentSection.StudentId });
            }
            var statustypes = _context.StatusTypes.Single(x => x.Name.Contains("Submitted"));
            studentSection.StatusTypesId = statustypes.Id;
            _context.StudentSections.Update(studentSection);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Session was submitted";
            return RedirectToAction("StudentPlanning", new { id = studentSection.StudentId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSection(int studentSectionId)
        {
			var studentSection = _context.StudentSections.Single(x => x.Id == studentSectionId);
			if (studentSection == null)
			{
				TempData["Message"] = "Error removing session";
				return RedirectToAction("StudentPlanning", new { id = studentSection.StudentId });
			}
			_context.StudentSections.Remove(studentSection);
			await _context.SaveChangesAsync();
			TempData["Message"] = "Session was removed successfully";
			return RedirectToAction("StudentPlanning", new { id = studentSection.StudentId });
		}
        //Method to fetch Sections based on courseId
        [HttpGet]
        public async Task<JsonResult> GetSections(int courseId)
        {
            var sections = await _context.Sections.Where(s => s.CourseId == courseId).ToListAsync();
            return Json(new SelectList(sections,"Id","DisplayName"));
        }

    }
}
