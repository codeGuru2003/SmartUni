namespace SmartUni.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }
        public string GetCurrentUser()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			return contextAccessor.HttpContext.User.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
		}
    }
}
