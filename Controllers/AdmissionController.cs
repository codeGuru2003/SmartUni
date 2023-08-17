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
        public AdmissionController(ApplicationDbContext context)
        {
			_context = context;
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
						check.CountyOfHighSchoolAttended = model.CountyOfHighSchoolAttended;
						check.HighSchoolAttendedName = model.HighSchoolAttendedName;
						check.StartYear = model.StartYear;
						check.EndYear = model.EndYear;
						_context.EntranceApplicants.Update(check);
						await _context.SaveChangesAsync();
						TempData["RecordSavedMessage"] = "Record has been updated.";
						return RedirectToAction("ProgramInformation");
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
		public IActionResult ProgramInformation()
		{
			var offeringTypes = _context.OfferingTypes.ToList();
			var OfferingTypeList = new List<SelectListItem>
			{
				new SelectListItem { Value = null, Text = "---- Select Offering Type -----" }
			};
			OfferingTypeList.AddRange(offeringTypes.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Name,
			}));

			ViewData["OfferingType"] = OfferingTypeList;	
			return View();
		}

		[AdmissionFilter]
		// POST: MultiStepForm/Step3
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ProgramInformation(MultiformDataViewModel formData)
		{
			// Retrieve Step 1 and Step 2 data from TempData
			var Biodata = TempData["Biodata"] as MultiformDataViewModel;
			var EducationalBackground = TempData["Biodata"] as MultiformDataViewModel;

			// Merge Step 1, Step 2, and Step 3 data
			formData.FirstName = Biodata.FirstName;
			formData.LastName = Biodata.LastName;
			formData.DateofBirth = Biodata.DateofBirth;
			//formData.University = step2Data.University;
			//formData.Degree = step2Data.Degree;
			//formData.GraduationYear = step2Data.GraduationYear;
			TempData["Step3Data"] = formData;
			return View("Summary", formData);
		}

		[AdmissionFilter]
		[HttpGet]
		public IActionResult References()
		{
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
		public IActionResult SupportingDocument()
		{
			return View();
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
    }
}
