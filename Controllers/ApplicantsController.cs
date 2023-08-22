using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Filters;

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
            var applicants = _context.EntranceApplicants.Include(x => x.StatusType).Where(x => x.StatusType.Name.Contains("Pending"));
            return View(await applicants.ToListAsync());
        }
    }
}
