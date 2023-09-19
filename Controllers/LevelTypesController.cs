using Microsoft.AspNetCore.Mvc;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class LevelTypesController : Controller
    {
        private readonly ApplicationDbContext context;

        public LevelTypesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var leveltypes = context.LevelTypes.ToList();
            return View(leveltypes);
        }
        [HttpPost]
        public IActionResult Create(LevelType leveltype)
        {
            if (ModelState.IsValid)
            {
                context.LevelTypes.Add(leveltype);
                context.SaveChanges();
                TempData["Message"] = "Record created successfully";
                return Redirect(nameof(Index));
            }
            TempData["Message"] = "Error creating record";
            return Redirect(nameof(Index));
        }
    }
}
