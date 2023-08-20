using Microsoft.AspNetCore.Mvc;
using SmartUni.Reports;

namespace SmartUni.Controllers
{
	public class ReportController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult ReportViewer()
		{
			var report = new Report1(); // Instantiate your report class
			return View(report);
		}

		public IActionResult ReportDesigner()
		{
			return PartialView("ReportDesigner");
		}
	}
}
