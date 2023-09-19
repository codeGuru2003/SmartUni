using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class SectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SectionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var sections = await _context.Sections
                .Include(s=>s.Course)
                .Include(s=>s.SectionType)
                .ToListAsync();
            return View(sections);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var section = await _context.Sections.SingleOrDefaultAsync(x => x.Id == Id);
            if (section == null)
            {
                return RedirectToAction("Index");
            }
            return View(section);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseName");
            ViewData["SectionTypeId"] = new SelectList(_context.SectionTypes, "Id", "Name");
            var faculties = _context.Faculties.ToList();
            var facultyList = faculties.Select(faculty => new SelectListItem
            {
                Value = faculty.Id.ToString(),
                Text = $"{faculty.LastName} {faculty.FirstName} {faculty.MiddleName}"
            }).ToList();
            ViewData["FacultyId"] = new SelectList(facultyList, "Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sections section)
        {
            if (ModelState.IsValid)
            {
                var sectiontype = await _context.SectionTypes.SingleOrDefaultAsync(x => x.Id.Equals(section.SectionTypeId));
                var academicsemester = await _context.AcademicSemesters.SingleOrDefaultAsync(x=>x.IsActive == true);
                section.AcademicSemesterId = academicsemester.Id;
                section.DisplayName = sectiontype.Name;
                section.Description = sectiontype.Name;
                _context.Sections.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var section = await _context.Sections.SingleOrDefaultAsync(x => x.Id == Id);
            if (section == null)
            {
                return RedirectToAction("Index");
            }
            return View(section);
        }
    }
}
