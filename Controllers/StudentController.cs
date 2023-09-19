﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Filters;
using SmartUni.Models;
using SmartUni.Services;
using SmartUni.ViewModels;

namespace SmartUni.Controllers
{
	public class StudentController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRandomStringGenerator _randomString;


		public StudentController(ApplicationDbContext context, 
			UserManager<ApplicationUser> userManager, IRandomStringGenerator randomString) 
		{ 
			_context = context;
			_userManager = userManager;
			_randomString = randomString;
		}
		
		public async Task<IActionResult> Index()
		{
			var students = await _context.Students.ToListAsync();
			return View(students);
		}
		public IActionResult Create()
		{
			ViewData["TitleID"] = new SelectList(_context.TitleTypes, "Id", "Name");
			ViewData["GenderID"] = new SelectList(_context.Genders, "Id", "Name");
			ViewData["NationalityID"] = new SelectList(_context.NationalityTypes, "Id", "Name");
			ViewData["CountryID"] = new SelectList(_context.CountryTypes, "Id", "Name");
			ViewData["ReligionID"] = new SelectList(_context.ReligionTypes, "Id", "Name");
			ViewData["MaritalStatusID"] = new SelectList(_context.MaritalStatusTypes, "Id", "Name");
			ViewData["OccupationID"] = new SelectList(_context.OccupationTypes, "Id", "Name");
			ViewData["RelationshipID"] = new SelectList(_context.RelationshipTypes, "Id", "Name");
			ViewData["DisabilityID"] = new SelectList(_context.DisabilityTypes, "Id", "Name");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Student student)
		{
			if (ModelState.IsValid)
			{

			}
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

		long generateStudentId(Student lastStudentId)
		{
			if (lastStudentId == null)
			{
				return (0001);
			}
			return lastStudentId.StudentId + 1;

		}

		public IActionResult ManagePlanning(string? keyword)
		{
			if (!String.IsNullOrEmpty(keyword))
			{
                var student = _context.Students.FirstOrDefault(x => x.StudentId.Equals(Convert.ToInt64(keyword)));
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

		string generatePassword()
		{
			string pwd;
			var ran1 = _randomString.GenerateRandomString(2);
			var ran2 = _randomString.GenerateRandomString(2);

			pwd = "P@" + ran1 + "w" + ran2 + "rd";
			return (pwd);
		}
	}

    

	
}
