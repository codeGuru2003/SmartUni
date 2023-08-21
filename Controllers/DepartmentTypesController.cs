using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;

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
    }
}
