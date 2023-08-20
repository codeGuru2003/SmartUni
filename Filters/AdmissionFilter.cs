using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SmartUni.Filters
{
	public class AdmissionFilter : ActionFilterAttribute
	{

        public override void OnActionExecuting(ActionExecutingContext context)
		{
			var session = context.HttpContext.Session;

			if (session == null || !session.Keys.Contains("Token"))
			{
				context.Result = new RedirectToActionResult("Apply", "Admission",null);
			}
		}
	}
}
