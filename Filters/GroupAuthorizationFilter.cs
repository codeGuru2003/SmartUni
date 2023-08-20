using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SmartUni.Filters
{
    public class GroupAuthorizationFilter : ActionFilterAttribute
    {
        private readonly string[] _allowedGroups;
        public GroupAuthorizationFilter(params string[] allowedGroups)
        {
            _allowedGroups = allowedGroups ?? throw new ArgumentNullException(nameof(allowedGroups));
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            var hasAllowedGroup = user.Claims.Any(claim =>
                claim.Type == "GroupId" && _allowedGroups.Contains(claim.Value));

            if (!hasAllowedGroup)
            {
                context.Result = new ForbidResult();
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
