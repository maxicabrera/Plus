using System.Web.Mvc;

namespace Plus54PortfolioRedesign2014.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context != null)
            {
                context.MapRoute(
                    "Admin_default",
                    "admin/{controller}/{action}/{id}",
                    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                    new[] { "Plus54PortfolioRedesign2014.Web.Areas.Admin.Controllers" }
                );
            }
        }
    }
}
