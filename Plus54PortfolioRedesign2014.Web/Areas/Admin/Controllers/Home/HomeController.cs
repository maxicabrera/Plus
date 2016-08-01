using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using Plus54PortfolioRedesign2014.Resources.resources.global;
using Plus54PortfolioRedesign2014.Entities;
using Plus54PortfolioRedesign2014.Web.Properties;
using Plus54PortfolioRedesign2014.Common;

namespace Plus54PortfolioRedesign2014.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public string ShowCurrentUser()
        {
            string result = "DFORTES";
            return result;
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
