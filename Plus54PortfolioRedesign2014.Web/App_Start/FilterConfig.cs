using System.Web;
using System.Web.Mvc;

namespace Plus54PortfolioRedesign2014.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}