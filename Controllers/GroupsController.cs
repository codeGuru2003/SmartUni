using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	[Authorize]
	public class GroupsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly RoleManager<IdentityRole> _roleManager;
        public GroupsController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
			_context =  context;
			_roleManager = roleManager;
        }
		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var groups = _context.Groups;
			return View(await groups.ToListAsync());
		}

		[HttpGet]
		public IActionResult Details(int id)
		{
			var group = _context.Groups.Find(id);
			return View(group);
		}

		[HttpPost]
		public async Task<IActionResult> AddGroupRole(string roleId, int groupId )
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			if (role == null)
			{
				TempData["Message"] = "Role doesn't exist";
				return RedirectToAction("Details", new {id = groupId });
			}
			//var groupRole = await _context.GroupRoles.Where(x=>x.GroupID == groupId && x.RoleID.Contains(roleId)).FirstOrDefaultAsync();
			var newgroup = new GroupRole()
			{
				GroupID = groupId,
				RoleID = roleId,
			};
			_context.GroupRoles.Add(newgroup);
			await _context.SaveChangesAsync();
            TempData["Message"] = "Role added successfully";
            return RedirectToAction("Details", new { id = groupId });
		}

        [HttpPost]
        public async Task<IActionResult> RemoveGroupRole(string roleId, int groupId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
			var groupRole = await _context.GroupRoles.Where(x=>x.GroupID == groupId && x.RoleID == role.Id).FirstOrDefaultAsync();
            if (role == null)
            {
                TempData["Message"] = "Role doesn't exist";
                return RedirectToAction("Details", new { id = groupId });
            }
			//var groupRole = await _context.GroupRoles.Where(x=>x.GroupID == groupId && x.RoleID.Contains(roleId)).FirstOrDefaultAsync();
			_context.GroupRoles.Remove(groupRole);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Role removed successfully";
            return RedirectToAction("Details", new { id = groupId });
        }

    }
}
