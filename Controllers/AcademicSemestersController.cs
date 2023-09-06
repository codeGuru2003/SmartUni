using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class AcademicSemestersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AcademicSemestersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var academicsemesters = _context.AcademicSemesters.ToList();
            return View(academicsemesters);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AcademicSemester academicSemester)
        {
            if (ModelState.IsValid)
            {
                var validate = await _context.AcademicSemesters.Where(x => x.IsActive == true).FirstOrDefaultAsync();
                if (validate == null)
                {
                    _context.AcademicSemesters.Add(academicSemester);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "AcademicYearTypes", new { id = academicSemester.AcademicYearId });
                }
                else
                {
                    academicSemester.IsActive = false;
                    _context.AcademicSemesters.Add(academicSemester);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "AcademicYearTypes", new { id = academicSemester.AcademicYearId });
                }
            }
            return RedirectToAction("Details", "AcademicYearTypes", new { id = academicSemester.AcademicYearId });
        }
    }
}
