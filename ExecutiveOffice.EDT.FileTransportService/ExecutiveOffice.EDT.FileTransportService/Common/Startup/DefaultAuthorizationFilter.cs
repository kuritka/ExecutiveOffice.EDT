using Hangfire.Dashboard;

namespace ExecutiveOffice.EDT.FileTransportService.Common.Startup
{
    internal class DefaultAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
