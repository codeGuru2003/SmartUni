using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Filters;
using SmartUni.Models;
using SmartUni.Services;

namespace SmartUni.Controllers
{
    [GroupAuthorizationFilter("Super Admin")]
    public class ApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EMailService _mailService;
        public ApplicantsController(ApplicationDbContext context,UserManager<ApplicationUser> userManager, EMailService mailService)
        {
            _context = context;
            _userManager = userManager;
            _mailService = mailService;
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
        //Approve Applicant Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveApplicant(int applicantId)
        {
            var applicant = await _context.EntranceApplicants.Where(x => x.Id == applicantId).FirstOrDefaultAsync();
            if (applicant != null)
            {

				var status = await _context.StatusTypes.Where(s => s.Name.Contains("Approved")).FirstOrDefaultAsync();
                applicant.StatusID = status.Id;
                var currentID = GenerateStudentId().ToString();
                var studentIdentityUser = new ApplicationUser()
                {
                    UserName = currentID,
                    NormalizedUserName = currentID,
                    Email = applicant.EmailAddress,
                    NormalizedEmail = applicant.EmailAddress.ToUpper(),
                    ImageUrl = applicant.ImagePath,
                    LoginHint = currentID,
                };
                await _userManager.CreateAsync(studentIdentityUser, "P@55w0rd");
                var student = new Student()
                {
                    ApplicationUser = studentIdentityUser,
                    FirstName = applicant.FirstName,
                    MiddleName = applicant.MiddleName,
                    LastName = applicant.LastName,
                    StatusID = status.Id,
                    StudentId = Convert.ToInt64(currentID),
                    DisabilityTypeID = applicant.DisabilityTypeID,
                    GenderID = applicant.GenderID,
                    TitleID = applicant.TitleID,
                    MaritalStatusID = applicant.MaritalStatusID,
                    DepartmentID = applicant.DepartmentID,
                    DepartmentDegreeID = applicant.DepartmentDegreeID,
                    OfferingTypeID = applicant.OfferingTypeID,
                    NationalityID = applicant.NationalityID,
                    CountryID = applicant.CountryID,
                    RelationshipTypeID = applicant.ReligionID,
                    OccupationID = applicant.OccupationID,
                    ReligionID = applicant.ReligionID,
                    CollegeID = applicant.CollegeID,
                    UniversityCountryID = applicant.UniversityCountryID,
                    EntryYear = applicant.EntryYear,
                    HighSchoolAttendedName = applicant.HighSchoolAttendedName,
                    CountyOfHighSchoolAttended = applicant.CountyOfHighSchoolAttended,
                    VoucherCode = applicant.StudentId,
                };
                //await SendEmailAsync("pantoejr@gmail.com","Test Email","This is a Test Email");
                _context.EntranceApplicants.Update(applicant);
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
				TempData["Message"] = "Applicant was approved!";
				return RedirectToAction("Index", "Applicants");
			}
			TempData["Message"] = "Applicant not found!";
			return RedirectToAction("Details","Applicants", new { id = applicantId });
        }

        public long GenerateStudentId()
        {
            var lastStudent = _context.Students
                .OrderByDescending(s => s.StudentId)
                .FirstOrDefault();

            long newStudentId;

            if (lastStudent != null)
            {
                newStudentId = lastStudent.StudentId + 1;
            }
            else
            {
                newStudentId = 202300001;
            }

            if (newStudentId < 202300001 || newStudentId >= 1000000000)
            {
                throw new Exception("Generated student ID is out of range.");
            }
            return newStudentId;
        }
        public IActionResult TestEmail()
        {
            var mailData = new MailData
            {
                EmailToId = "joelpantoejr@gmail.com",
                EmailToName = "Joel Pantoe Jr",
                EmailSubject = "Test Email",
                EmailBody = "This is a test email",
            };
            var result = _mailService.SendMail(mailData);
            if (result == false)
            {
                TempData["Message"] = "Error";
                return View();
            }
            TempData["Message"] = "Mail Sent Successfully";
            return View();

        }
    }
}
