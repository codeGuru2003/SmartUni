using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;
using SmartUni.Services;
using System.Security.Cryptography.X509Certificates;

namespace SmartUni.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IRandomStringGenerator _random;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        public FacultyController(ApplicationDbContext context,UserManager<ApplicationUser> userManager,IWebHostEnvironment webHostEnvironment,IRandomStringGenerator random)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _random = random;
        }
        public async Task<IActionResult> Index()
        {
            var facultyList = await _context.Faculties
                .Include(x=>x.Nationality)
                .Include(x=>x.GenderType)
                .Include(x=>x.Department)
                .Include(x=>x.TitleType).Include(x=>x.FacultyType)
                .Include(x=>x.MaritalStatusType)
                .ToListAsync();
            return View(facultyList);
        }

        public IActionResult Create()
        {
            ViewData["FacultyTypeID"] = new SelectList(_context.FacultyTypes, "Id", "Name");
            ViewData["NationalityID"] = new SelectList(_context.NationalityTypes, "Id", "Name");
            ViewData["MaritalStatusTypeID"] = new SelectList(_context.MaritalStatusTypes, "Id", "Name");
            ViewData["TitleID"] = new SelectList(_context.TitleTypes, "Id", "Name");
            ViewData["GenderID"] = new SelectList(_context.Genders, "Id", "Name");
            ViewData["GroupID"] = new SelectList(_context.Groups, "Id", "Name");
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faculty faculty, String number, String email)
        {
            var imageFile = Request.Form.Files["photograph"];
            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadsRoot = Path.Combine(_webHostEnvironment.WebRootPath, "facultyphoto");
                if (!Directory.Exists(uploadsRoot))
                {
                    Directory.CreateDirectory(uploadsRoot);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsRoot, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                var applicationuser = new ApplicationUser()
                {
                    ImageUrl = uniqueFileName,
                    Email = email,
                    PhoneNumber = number,
                    UserName = $"{faculty.FirstName.ToLower()}{faculty.LastName.ToLower()}",
                    NormalizedUserName = $"{faculty.FirstName.ToUpper()}{faculty.LastName.ToUpper()}",
                    NormalizedEmail = email.ToUpper(),
                    LoginHint = $"{faculty.FirstName}{faculty.LastName.ToLower()}$52w0rd",

                };
                faculty.UserID = applicationuser.Id;
                var result = await _userManager.CreateAsync(applicationuser, $"{faculty.FirstName}{faculty.LastName.ToLower()}$52w0rd");
                await _context.Faculties.AddAsync(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FacultyTypeID"] = new SelectList(_context.FacultyTypes, "Id", "Name");
            ViewData["NationalityID"] = new SelectList(_context.NationalityTypes, "Id", "Name");
            ViewData["MaritalStatusTypeID"] = new SelectList(_context.MaritalStatusTypes, "Id", "Name");
            ViewData["TitleID"] = new SelectList(_context.TitleTypes, "Id", "Name");
            ViewData["GenderID"] = new SelectList(_context.Genders, "Id", "Name");
            ViewData["GroupID"] = new SelectList(_context.Groups, "Id", "Name");
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }
    }
}
