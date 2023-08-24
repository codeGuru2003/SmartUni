using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using SmartUni.Data;
using SmartUni.Filters;
using SmartUni.Models;
using SmartUni.ViewModels;
using System.Data;

namespace SmartUni.Controllers
{
	
	public class AdmissionController : Controller
	{
		
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
        public AdmissionController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
			_context = context;
			_webHostEnvironment = webHostEnvironment;
        }
		[HttpGet]
		public IActionResult Apply()
		{
			return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
        public async Task<IActionResult> Apply(string token)
		{
			if (!String.IsNullOrEmpty(token))
			{
				var check_token = await _context.Tokens.Where(x => x.Value.Contains(token)).FirstOrDefaultAsync();
				switch (check_token)
				{
					case null:
						TempData["Message"] = "Invalid Token";
						TempData["Type"] = "danger";
						return View();
					default:
						switch (check_token.HasEntered)
						{
							case true:
								HttpContext.Session.SetString("Token", token);
								TempData["LoginMessage"] = "Login successful";
								//HttpContext.Response.Cookies.Append("Token", token);
								return RedirectToAction("Biodata");
							case false:
								check_token.HasEntered = true;
								check_token.DateEntered = DateTime.Now;
								_context.Tokens.Update(check_token);
								await _context.SaveChangesAsync();
								HttpContext.Session.SetString("Token", token);
								TempData["LoginMessage"] = "Login successful";
								return RedirectToAction("Biodata", "Admission");
							default:
								// If the value of check_token.HasEntered is neither true nor false, handle the default case here.
								// You may throw an exception or take some other appropriate action.
								throw new InvalidOperationException("Invalid value for check_token.HasEntered.");
						}
				}
			}

			return View();
		}

		[AdmissionFilter]
		[HttpGet]
        public async Task<IActionResult> Biodata()
		{
			var session = HttpContext.Session.GetString("Token");
			var applicant = await _context.EntranceApplicants.Where(x => x.StudentId.Contains(session)).FirstOrDefaultAsync();
			if (applicant != null)
			{
                ViewData["TitleTypeId"] = new SelectList(_context.TitleTypes, "Id", "Name");
                ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
                ViewData["NationalityId"] = new SelectList(_context.NationalityTypes, "Id", "Name");
                ViewData["CountryId"] = new SelectList(_context.CountryTypes, "Id", "Name");
                ViewData["ReligionId"] = new SelectList(_context.ReligionTypes, "Id", "Name");
                ViewData["MaritalStatusId"] = new SelectList(_context.MaritalStatusTypes, "Id", "Name");
                ViewData["OccupationId"] = new SelectList(_context.OccupationTypes, "Id", "Name");
                ViewData["RelationshipId"] = new SelectList(_context.RelationshipTypes, "Id", "Name");
                ViewData["DisabilityId"] = new SelectList(_context.DisabilityTypes, "Id", "Name");
                return View(applicant);
            }
			//var titleTypes = await _context.TitleTypes.ToListAsync();
			//var selectList = new List<SelectListItem>
			//{
			//	new SelectListItem {Value = null, Text = "------------ Select Title -------------"}
			//};
			//selectList.AddRange(titleTypes.Select(items => new SelectListItem
			//{
			//		Value = items.Id.ToString(), Text = items.Name
			//}));
			ViewData["TitleTypeId"] = new SelectList(_context.TitleTypes, "Id", "Name");
			ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
			ViewData["NationalityId"] = new SelectList(_context.NationalityTypes, "Id", "Name");
			ViewData["CountryId"] = new SelectList(_context.CountryTypes, "Id", "Name");
			ViewData["ReligionId"] = new SelectList(_context.ReligionTypes, "Id", "Name");
			ViewData["MaritalStatusId"] = new SelectList(_context.MaritalStatusTypes, "Id", "Name");
			ViewData["OccupationId"] = new SelectList(_context.OccupationTypes, "Id", "Name");
			ViewData["RelationshipId"] = new SelectList(_context.RelationshipTypes, "Id", "Name");
			ViewData["DisabilityId"] = new SelectList(_context.DisabilityTypes, "Id", "Name");
			return View();
		}

		[AdmissionFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Biodata(EntranceApplicant applicant)
		{
            var session = HttpContext.Session.GetString("Token");
			var check = await _context.EntranceApplicants.Where(x => x.StudentId.Contains(session)).FirstOrDefaultAsync();
			if (ModelState.IsValid)
			{
                switch (check)
                {
                    case not null:
						check.TitleID = applicant.TitleID;
						check.FirstName = applicant.FirstName;
						check.MiddleName = applicant.MiddleName;
						check.GenderID = applicant.GenderID;
						check.DateofBirth = applicant.DateofBirth;
						check.Hometown_State = applicant.Hometown_State;
						check.NationalityID = applicant.NationalityID;
						check.CountryID = applicant.CountryID;
						check.ReligionID = applicant.ReligionID;
                        _context.EntranceApplicants.Update(check);
						await _context.SaveChangesAsync();
						TempData["RecordSavedMessage"] = "The record has been updated.";
						return RedirectToAction("EducationalBackground");
                    default:
                        var status = await _context.StatusTypes.Where(x => x.Name.Contains("Pending")).FirstOrDefaultAsync();
                        applicant.StatusType = status;
                        applicant.StudentId = session;
                        _context.EntranceApplicants.Add(applicant);
                        await _context.SaveChangesAsync();
						TempData["RecordSavedMessage"] = "The record has been saved.";
						return RedirectToAction("EducationalBackground");
                }
            }
            ViewData["TitleTypeId"] = new SelectList(_context.TitleTypes, "Id", "Name", applicant.TitleID);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", applicant.GenderID);
            ViewData["NationalityId"] = new SelectList(_context.NationalityTypes, "Id", "Name", applicant.NationalityID);
            ViewData["CountryId"] = new SelectList(_context.CountryTypes, "Id", "Name", applicant.CountryID);
            ViewData["ReligionId"] = new SelectList(_context.ReligionTypes, "Id", "Name", applicant.ReligionID);
            ViewData["MaritalStatusId"] = new SelectList(_context.MaritalStatusTypes, "Id", "Name", applicant.MaritalStatusID);
            ViewData["OccupationId"] = new SelectList(_context.OccupationTypes, "Id", "Name", applicant.OccupationID);
            ViewData["RelationshipId"] = new SelectList(_context.RelationshipTypes, "Id", "Name", applicant.RelationshipTypeID);
            ViewData["DisabilityId"] = new SelectList(_context.DisabilityTypes, "Id", "Name", applicant.DisabilityTypeID);
            TempData["Error"] = "Could not execute task";
            return View();
        }

		[AdmissionFilter]
		// GET: MultiStepForm/Step2
		public async Task<IActionResult> EducationalBackground()
		{
			var country = _context.CountryTypes.ToList();
			var selectList = new List<SelectListItem>
			{
				new SelectListItem { Value = null, Text = "------ Select Country --------" }
			};
			selectList.AddRange(country.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Name,
			}));

			var session = HttpContext.Session.GetString("Token");
			var applicant = await _context.EntranceApplicants.Where(x => x.StudentId.Contains(session)).FirstOrDefaultAsync();
			if (applicant != null)
			{
				ViewData["CountryId"] = selectList;
				return View(applicant);
			}
			ViewData["CountryId"] = selectList;
			return View();
		}

		// POST: MultiStepForm/Step2
		[AdmissionFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EducationalBackground(EntranceApplicant model)
		{
			var session = HttpContext.Session.GetString("Token");
			var check = await _context.EntranceApplicants.Where(x => x.StudentId.Contains(session)).FirstOrDefaultAsync();
			if (model != null)
			{
				switch (check)
				{
					case null:
						TempData["RecordSavedMessage"] = "Applicant does not exist";
						return RedirectToAction("EducationalBackground");
					default:
						if (model.NameOfUniversity != null && model.UniversityCountryID != null && model.UniversityStartYear != null && model.UniversityEndYear != null)
						{
							check.CountyOfHighSchoolAttended = model.CountyOfHighSchoolAttended;
							check.HighSchoolAttendedName = model.HighSchoolAttendedName;
							check.StartYear = model.StartYear;
							check.EndYear = model.EndYear;
							check.NameOfUniversity = model.NameOfUniversity;
							check.UniversityCountryID = Convert.ToInt32(model.UniversityCountryID);
							check.UniversityStartYear = model.UniversityStartYear;
							check.UniversityEndYear = model.UniversityEndYear;
							_context.EntranceApplicants.Update(check);
							await _context.SaveChangesAsync();
							TempData["RecordSavedMessage"] = "Record has been updated.";
							return RedirectToAction("ProgramInformation");
						}
						TempData["RecordSavedMessage"] = "Could not update record.";
						return RedirectToAction("EducationalBackground");
				}
			}
			var country = _context.CountryTypes.ToList();
			var selectList = new List<SelectListItem>
			{
				new SelectListItem { Value = null, Text = "------ Select Country --------" }
			};
			selectList.AddRange(country.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Name,
			}));

			ViewData["CountryId"] = selectList;
			TempData["RecordSaveMessage"] = "Error saving record";
			return RedirectToAction("EducationalBackground");
		}

		[AdmissionFilter]
		// GET: MultiStepForm/Step3
		public async Task<IActionResult> ProgramInformation()
		{
			var session = HttpContext.Session.GetString("Token");
			var applicant = await _context.EntranceApplicants.Where(x => x.StudentId.Contains(session)).FirstOrDefaultAsync();
			if (applicant != null)
			{
				ViewData["CollegeID"] = new SelectList(_context.College, "Id", "Name");
				ViewData["OfferingTypeId"] = new SelectList(_context.OfferingTypes, "Id", "Name");
				return View(applicant);
			}
			ViewData["CollegeID"] = new SelectList(_context.College,"Id","Name");
			ViewData["OfferingTypeId"] = new SelectList(_context.OfferingTypes, "Id", "Name");
			return View();
		}

		[AdmissionFilter]
		// POST: MultiStepForm/Step3
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ProgramInformation(EntranceApplicant model, string DegreeId)
		{
			var session = HttpContext.Session.GetString("Token");
			var check = await _context.EntranceApplicants.Where(x => x.StudentId.Contains(session)).FirstOrDefaultAsync();
			if (model != null)
			{
				switch (check)
				{
					case null:
						TempData["RecordSavedMessage"] = "Applicant does not exist";
						return RedirectToAction("EducationalBackground");
					default:
						if (model.OfferingTypeID != null && model.CollegeID != null && model.DepartmentID != null && DegreeId != null)
						{
							check.OfferingTypeID = model.OfferingTypeID;
							check.CollegeID = model.CollegeID;
							check.DepartmentID = model.DepartmentID;
							check.DepartmentDegreeID = Convert.ToInt32(DegreeId);
							check.Scholarship = model.Scholarship;
							check.EntryYear = model.EntryYear;
							_context.EntranceApplicants.Update(check);
							await _context.SaveChangesAsync();
							TempData["RecordSavedMessage"] = "Record has been updated.";
							return RedirectToAction("References");
						}
						TempData["RecordSavedMessage"] = "Could not update record.";
						return RedirectToAction("ProgramInformation");
				}
			}
			ViewData["CollegeID"] = new SelectList(_context.College, "Id", "Name");
			ViewData["OfferingTypeId"] = new SelectList(_context.OfferingTypes, "Id", "Name");
			return View();
		}

		[AdmissionFilter]
		[HttpGet]
		public async Task<IActionResult> References()
		{
			var session = HttpContext.Session.GetString("Token");
			var check = await _context.EntranceApplicants.Where(x => x.StudentId.Contains(session)).FirstOrDefaultAsync();
			return View(check);
		}

		[AdmissionFilter]
		// POST: MultiStepForm/Step3
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> References(EntranceApplicant applicant)
		{
			var session = HttpContext.Session.GetString("Token");
			var check = await _context.EntranceApplicants.Where(x => x.StudentId.Contains(session)).FirstOrDefaultAsync();
			if (ModelState.IsValid)
			{

			}
			return View();
		}
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("Token");
			HttpContext.Session.Clear();
			return RedirectToAction("Apply", "Admission");
		}

		public bool ApplicantExists(string token)
		{
			var applicant = _context.EntranceApplicants.Where(x => x.StudentId.Contains(token)).FirstOrDefault();
			if (applicant == null)
			{
				return false;
			}
			return true;
		}

		[AdmissionFilter]
		[HttpGet]
		public async Task<IActionResult> SupportingDocument()
		{
			var session = HttpContext.Session.GetString("Token");
			var check = await _context.EntranceApplicants.Where(x => x.StudentId.Contains(session)).FirstOrDefaultAsync();
			return View(check);
		}

        [AdmissionFilter]
        [HttpGet]
        public IActionResult UploadPhoto()
        {
            return View();
        }

        [AdmissionFilter]
        [HttpGet]
        public IActionResult Summary()
        {
            return View();
        }

        [AdmissionFilter]
        [HttpGet]
        public IActionResult ProofOfApplication()
        {
            return View();
        }

        [AdmissionFilter]
        [HttpGet]
        public IActionResult Status()
        {
            return View();
        }
		public async Task<JsonResult> GetDepartments(int collegeId)
		{
			var departments = await _context.Departments
				.Where(d=>d.CollegeID == collegeId)
				.ToListAsync();
			return Json(new SelectList(departments, "Id", "Name"));
		}
		public async Task<JsonResult> GetDegrees(int departmentId)
		{
			var degrees = await _context.DepartmentDegrees
				.Where(d => d.DepartmentID == departmentId)
				.ToListAsync();
			return Json(new SelectList(degrees, "Id", "Name"));
		}

		[AdmissionFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UploadDocument(IFormFile file, DocumentUploadViewModel model)
		{

			if (file == null || file.Length == 0)
				return RedirectToAction("Index");

			var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
			if (!Directory.Exists(uploadsPath))
				Directory.CreateDirectory(uploadsPath);

			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			var filePath = Path.Combine(uploadsPath, fileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}
			var entranceApplicantDocument = new EntranceApplicantDocument
			{
				EntranceApplicantId = model.EntranceApplicantId,
				DocumentTypeId = model.DocumentTypeId,
				FilePath = filePath,
			};
			_context.EntranceApplicantDocuments.Add(entranceApplicantDocument);
			await _context.SaveChangesAsync();
			return RedirectToAction("SupportingDocument");
		}

		[AdmissionFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteDocument(int id)
		{
			var pdfFile = await _context.EntranceApplicantDocuments.FindAsync(id);
			if (pdfFile == null)
				return NotFound();
			if (System.IO.File.Exists(pdfFile.FilePath))
			{
				System.IO.File.Delete(pdfFile.FilePath);
			}

			_context.EntranceApplicantDocuments.Remove(pdfFile);
			await _context.SaveChangesAsync();
			return RedirectToAction("SupportingDocument");
		}
	}
}
