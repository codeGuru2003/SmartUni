using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;
using SmartUni.ViewModels;
using System.Data;
using System.Security.Claims;

namespace SmartUni.Controllers
{
    public class AccountController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_context = context;
		}
		public IActionResult Index()
        {
			var userlist = _context.Users.ToList();
            return View(userlist);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Username,
				   model.Password, false, false);

				if (result.Succeeded)
				{
					var groups = await _context.UserGroups.ToListAsync();
                    var user = await _signInManager.UserManager.FindByNameAsync(model.Username);
					var usergroup = await _context.UserGroups.Where(u => u.UserID.Equals(user.Id)).Include(x=>x.Group).FirstOrDefaultAsync();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
					if (usergroup.Group.Name.Contains("Super Admin"))
					{
                        var claims = new[]
						{
							new Claim("GroupId",$"{ usergroup.Group.Name }")
						};
                        await _signInManager.UserManager.AddClaimsAsync(user, claims);
                        return RedirectToAction("Index", "Home");
                    }
					else if (usergroup.Group.Name.Contains("Student"))
					{
                        var claims = new[]
                        {
                            new Claim("GroupId",$"{ usergroup.Group.Name }")
                        };
                        await _signInManager.UserManager.AddClaimsAsync(user, claims);
                        return RedirectToAction("Default", "Student");
                    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
				}
				else
				{
					TempData["Error"] = "Incorrect Username or Password";
					return RedirectToAction("Login");
				}
			}
			return View(model);
		}

		[ActionName("Logout")]
		public async Task<IActionResult> LogoutAsync()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Account");
		}

		[HttpGet]
		public async Task<IActionResult> Profile()
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Redirect("Login");
			}
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.GetUserAsync(User);
					var result = await _userManager.ChangePasswordAsync(user, model.Old_Password, model.New_Password);
					if (result.Succeeded)
					{
						user.LoginHint = model.New_Password;
						_context.Users.Update(user);
						await _context.SaveChangesAsync();
                        await _signInManager.SignOutAsync();
                        TempData["Message"] = "Password was changed successfully";
						return RedirectToAction("Login","Account");
					}
				}
				catch (Exception)
				{
                    return RedirectToAction("Profile", "Account");
                }
			}
            return RedirectToAction("Profile", "Account");
        }
		//[Authorize(Roles = "SuperAdmin, Administrator")]
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public IActionResult DeleteRole(string roleName, string userId)
		//{
		//	var user = _userManager.FindByIdAsync(userId).Result;
		//	var check = user.UserName.Contains("superadmin");
		//	if (user != null /*&& check == false*/)
		//	{
		//		var result = _userManager.RemoveFromRoleAsync(user, roleName).Result;
		//		if (result.Succeeded)
		//		{
		//			TempData["Message"] = "Role was removed successfully";
		//			// Role removal successful
		//			return RedirectToAction("Details", new { Id = userId });
		//		}
		//	}
		//	TempData["Message"] = "Fail to remove role from user";
		//	// Role removal failed
		//	return RedirectToAction("Details", new { Id = userId });
		//}
		[HttpGet]
		public async Task<IActionResult> Details(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			return View(user);
		}
		public async Task<IActionResult> AddUserToGroup(string UserID, int groupId)
		{
			if (UserID == null || groupId <= 0)
			{
				TempData["Message"] = "Could not process User ID or group is null";
				return RedirectToAction("Details", new { id = UserID });
			}
			var user = await _userManager.FindByIdAsync(UserID);
			var usergroup = new UserGroup()
			{
				UserID = user.Id,
				GroupID = groupId
			};
			_context.UserGroups.Add(usergroup);
			await _context.SaveChangesAsync();
            TempData["Message"] = "User added to group successfully";
            return RedirectToAction("Details", new { id = UserID });
        }
		[HttpGet]
		public IActionResult AccessDenied(string? returnurl)
		{
			return View();
		}
	}
}
