using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;
using SmartUni.ViewModels;

namespace SmartUni.Controllers
{
	public class StudentController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		
		public StudentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) 
		{ 
			_context = context;
			_userManager = userManager;
		}
		public async Task<IActionResult> Index()
		{
			var students = await _context.Students.ToListAsync();
			return View(students);
		}
		public IActionResult Create()
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
            return View();
		}
		public IActionResult Create(Student student)
		{
			return View();
		}
		[HttpGet]
		public IActionResult Biodata()
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
            return View();
     
		}
		[HttpPost]
		public async Task<IActionResult> Biodata(Student student, bool IsNew)
		{
			
			var session = HttpContext.Session.GetString("a");
            if (ModelState.IsValid)
            {
                if(IsNew)
                {
					var lastStudentId = await _context.Students.LastAsync();
                    var stID = generateStudentId(lastStudentId);
					student.StudentId = stID;
                } 
                
                var status = await _context.StatusTypes.Where(x => x.Name.Contains("Pending")).FirstOrDefaultAsync();
				student.StatusType = status;
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                TempData["RecordSavedMessage"] = "The record has been saved.";
                return RedirectToAction("EducationalBackground", "Student", new { studentId1 = student.Id });

            }
            ViewData["TitleTypeId"] = new SelectList(_context.TitleTypes, "Id", "Name", student.TitleID);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", student.GenderID);
            ViewData["NationalityId"] = new SelectList(_context.NationalityTypes, "Id", "Name", student.NationalityID);
            ViewData["CountryId"] = new SelectList(_context.CountryTypes, "Id", "Name", student.CountryID);
            ViewData["ReligionId"] = new SelectList(_context.ReligionTypes, "Id", "Name", student.ReligionID);
            ViewData["MaritalStatusId"] = new SelectList(_context.MaritalStatusTypes, "Id", "Name", student.MaritalStatusID);
            ViewData["OccupationId"] = new SelectList(_context.OccupationTypes, "Id", "Name", student.OccupationID);
            ViewData["RelationshipId"] = new SelectList(_context.RelationshipTypes, "Id", "Name", student.RelationshipTypeID);
            ViewData["DisabilityId"] = new SelectList(_context.DisabilityTypes, "Id", "Name", student.DisabilityTypeID);
            TempData["Error"] = "Could not execute task";
            return View();

		}


		public async Task<IActionResult> EducationalBackground(int? studentId1)
		{
			if (studentId1 == null)
			{
				return RedirectToAction("Biodata");
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

			var student = await _context.Students.Where(x => x.Id == studentId1).FirstOrDefaultAsync();
			if (student != null)
			{
				ViewData["CountryId"] = selectList;
				ViewData["AddstudentId"] = studentId1;
                return View(student);
			}
			ViewData["AddstudentId"] = studentId1;
            ViewData["CountryId"] = selectList;
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EducationalBackground(Student model, int? studentId1)
		{
			if (studentId1 == null)
			{
				return RedirectToAction("Biodata");
			}
			var check = await _context.Students.Where(x => x.Id == studentId1).FirstOrDefaultAsync();
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
							_context.Students.Update(check);
							await _context.SaveChangesAsync();
							TempData["RecordSavedMessage"] = "Record has been updated.";
							return RedirectToAction("ProgramInformation");
						}
						check.CountyOfHighSchoolAttended = model.CountyOfHighSchoolAttended;
						check.HighSchoolAttendedName = model.HighSchoolAttendedName;
						check.StartYear = model.StartYear;
						check.EndYear = model.EndYear;
						_context.Students.Update(check);
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
			ViewData["AddstudentId"] = studentId1;
			TempData["RecordSaveMessage"] = "Error saving record";
			return RedirectToAction("EducationalBackground", "Student", new {studentId1 = studentId1});
		}







		long generateStudentId(Student lastStudentId)
		{
            if (lastStudentId == null)
            {
				return (0001);
			}
			return lastStudentId.StudentId + 1;
			
		}
	}

    

	
}
