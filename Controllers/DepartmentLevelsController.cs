using Microsoft.AspNetCore.Mvc;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class DepartmentLevelsController : Controller
    {
        private readonly ApplicationDbContext context;

        public DepartmentLevelsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentLevel departmentlevel)
        {
            context.DepartmentLevels.Add(departmentlevel);
            context.SaveChanges();
            return Redirect(nameof(Index));
        }
        public IActionResult Edit()
        {
            return View();
        }
    }
}
