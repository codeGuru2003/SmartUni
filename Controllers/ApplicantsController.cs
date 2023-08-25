using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Filters;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    [GroupAuthorizationFilter("Super Admin")]
    public class ApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ApplicantsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var applicants = _context.EntranceApplicants.Include(x => x.StatusType).Include(x=>x.TitleType).Include(x=>x.DepartmentDegree).Where(x => x.StatusType.Name.Contains("Pending"));
            return View(await applicants.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var applicant = await _context.EntranceApplicants.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (applicant == null)
            {
                TempData["Message"] = "Record not found";
                return Redirect("Index");
            }
            return View(applicant);
        }
    }
}
