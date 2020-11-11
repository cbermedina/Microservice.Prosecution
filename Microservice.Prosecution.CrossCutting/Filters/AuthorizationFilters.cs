namespace Microservice.Prosecution.CrossCutting.Filters
{
    using Hangfire.Dashboard;
    public class AuthorizationFilters : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// Metodo encargado de verificar si se esta autorizado para ver el dashboard
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool Authorize(DashboardContext context)
        {
            //var httpContext = context.GetHttpContext();
            //return httpContext.User.Identity.IsAuthenticated;
            return true;
        }
    }
}
