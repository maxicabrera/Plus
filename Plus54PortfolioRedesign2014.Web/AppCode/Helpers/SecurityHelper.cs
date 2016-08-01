using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plus54PortfolioRedesign2014.Web.Properties;
using Plus54PortfolioRedesign2014.BusinessLogic;

namespace Plus54PortfolioRedesign2014.Web.AppCode.Helpers
{
    public static class SecurityHelper
    {
        public static bool CustomAuthorizeCore(AuthorizationContext context = null)
        {
            bool result = false;

            if (SessionHelper.IsSignedIn)
                return true;

            return result;
        }

        private static string GenerateUrlFromControllerAction(System.Web.Mvc.AuthorizationContext filterContext)
        {
            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string action = filterContext.ActionDescriptor.ActionName;

            return string.Format(CultureInfo.InvariantCulture, "/{0}/{1}", controller, action).ToLower();
        }
    }
}