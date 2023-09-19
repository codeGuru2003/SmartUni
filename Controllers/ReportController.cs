using FastReport;
using FastReport.Data;
using FastReport.Utils;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SmartUni.Data;
using System.Configuration;

namespace SmartUni.Controllers
{
	public class ReportController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly ApplicationDbContext _context;
        public ReportController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
			_configuration = configuration;
        }
        public IActionResult Index()
		{
			return View();
		}
		public IActionResult ApplicantSummary(int Id)
		{
			var webReport = new WebReport();
			var mssqlDataConnection = new MsSqlDataConnection();
			mssqlDataConnection.ConnectionString = _configuration.GetConnectionString("Default");
			webReport.Report.Dictionary.Connections.Add(mssqlDataConnection);
            string reportFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports", "EntranceApplicantReport.frx");
            webReport.Report.Load(reportFilePath);
            webReport.Report.SetParameterValue("@applicantId", Id);
          //  var applicantSummary = _context.GetEntranceApplicantDetailsById(Id);
//			webReport.Report.RegisterData(applicantSummary, "Entrance Applicant");
			return View(webReport);
		}
	}
}
