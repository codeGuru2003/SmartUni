using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartUni.Data;
using SmartUni.Models;
using SmartUni.ViewModels;
using System.Data;

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
            return View();
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
					return RedirectToAction("Index", "Home");
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
	}
}
