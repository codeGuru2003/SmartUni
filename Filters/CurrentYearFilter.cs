using Microsoft.AspNetCore.Mvc.Filters;
using SmartUni.Data;

namespace SmartUni.Filters
{
    public class CurrentYearFilter : IActionFilter
    {
        private readonly ApplicationDbContext _context;
        public CurrentYearFilter(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var current = _context.AcademicSemesters.Where(x=>x.IsActive == true).First();
            throw new NotImplementedException();
        }
    }
}
