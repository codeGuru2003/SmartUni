using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var departments = _context.Departments;
            return View(await departments.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var department = _context.Departments.Where(x=>x.Id == Id).FirstOrDefault();
            if (department != null)
            {
                return View(department);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (department.IsFlatRate == false)
            {
                department.Money = null;
                if (ModelState.IsValid)
                {
                    _context.Departments.Add(department);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Record was created sucessfully";
                    return RedirectToAction("Details", "Colleges", new { id = Convert.ToInt32(TempData["CollegeID"]) });
                }
                TempData["Message"] = "Error creating record";
                return RedirectToAction("Details", "Colleges", new { id = Convert.ToInt32(TempData["CollegeID"]) });
            }
            else 
            { 
                if (ModelState.IsValid)
                {
                    _context.Departments.Add(department);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Record was created sucessfully";
                    return RedirectToAction("Details", "Colleges", new { id = Convert.ToInt32(TempData["CollegeID"]) });
                }
                TempData["Message"] = "Error creating record";
                return RedirectToAction("Details", "Colleges", new { id = Convert.ToInt32(TempData["CollegeID"]) });
            }
        }
    }
}
