using Microsoft.AspNetCore.Mvc;
using SmartUni.Data;
using SmartUni.Models;

namespace SmartUni.Controllers
{
	public class BillingItemsController : Controller
	{
		private readonly ApplicationDbContext _context;
        public BillingItemsController(ApplicationDbContext context)
        {
			_context = context;
        }
        public IActionResult Index()
		{
			List<BillingItem> billingItems = _context.BillingItems.ToList();
			return View(billingItems);
		}
	}
}
