using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Data;
using System.Web.Routing;
using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;


namespace Plus54PortfolioRedesign2014.Web
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        #region Constructors

        public CustomAuthorizeAttribute() { }

        #endregion

        #region Public Methods

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (SecurityHelper.CustomAuthorizeCore(filterContext))
            {
                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
                this.HandleUnauthorizedRequest(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = 403;

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login" }));
        }

        #endregion

        #region Private Methods

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        #endregion
    }
}