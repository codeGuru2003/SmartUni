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
            var applicants = _context.EntranceApplicants.Include(x => x.StatusType).Include(x=>x.TitleType).Include(x=>x.DepartmentDegree).Where(x => x.StatusType.Name.Contains("Submitted"));
            return View(await applicants.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
			var applicant = await _context.EntranceApplicants.Where(x => x.Id == id)
				.Include(c => c.DepartmentDegree)
				.Include(b => b.GenderType)
				.Include(x => x.TitleType).FirstOrDefaultAsync();
            if (applicant == null)
            {
                TempData["Message"] = "Record not found";
                return Redirect("Index");
            }
            return View(applicant);
        }

        //Method to Disapprove Referee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeclineReferee(int refereeId, int applicantId)
        {
			var referee = await _context.EntranceApplicantReferences.Where(x => x.Id == refereeId).FirstOrDefaultAsync();
			if (referee != null)
			{
				var status = await _context.StatusTypes.Where(s => s.Name.Contains("Denied")).FirstOrDefaultAsync();
				referee.StatusTypeID = status.Id;
				_context.EntranceApplicantReferences.Update(referee);
				await _context.SaveChangesAsync();
				TempData["Message"] = "Referee was Denied!";
				return RedirectToAction("Details", new { id = applicantId });
			}
			TempData["Message"] = "Referee doesn't exist";
			return RedirectToAction("Details", new { id = applicantId });
		}
        //Method to Approve Referee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveReferee(int refereeId, int applicantId)
        {
            var referee = await _context.EntranceApplicantReferences.Where(x => x.Id == refereeId).FirstOrDefaultAsync();
            if (referee != null)
            {
                var status = await _context.StatusTypes.Where(s => s.Name.Contains("Approved")).FirstOrDefaultAsync();
                referee.StatusTypeID = status.Id;
                _context.EntranceApplicantReferences.Update(referee);
                await _context.SaveChangesAsync();
				TempData["Message"] = "Referee was approved!";
				return RedirectToAction("Details", new { id = applicantId });
			}
            TempData["Message"] = "Referee doesn't exist";
            return RedirectToAction("Details", new { id = applicantId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveApplicant(int applicantId)
        {
            var applicant = await _context.EntranceApplicants.Where(x => x.Id == applicantId).FirstOrDefaultAsync();
            if (applicant != null)
            {
				var status = await _context.StatusTypes.Where(s => s.Name.Contains("Approved")).FirstOrDefaultAsync();
                applicant.StatusID = status.Id;
                _context.EntranceApplicants.Update(applicant);
                await _context.SaveChangesAsync();
				TempData["Message"] = "Applicant was approved!";
				return RedirectToAction("Index", "Applicants");
			}
			TempData["Message"] = "Applicant not found!";
			return RedirectToAction("Details","Applicants", new { id = applicantId });
        }
    }
}
