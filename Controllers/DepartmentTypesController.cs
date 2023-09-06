using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class DepartmentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DepartmentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var departmenttypes = await _context.DepartmentTypes.ToListAsync();
            return View(departmenttypes);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(DepartmentType departmenttype)
        {
            if (ModelState.IsValid)
            {
                _context.DepartmentTypes.Add(departmenttype);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Department Type created successfully";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Error creating department type";
            return View();
        }
    }
}
