using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plus54PortfolioRedesign2014.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Error/Error404/
        public ActionResult Error404()
        {
            ViewBag.ErrorPath = Request.QueryString["aspxerrorpath"];

            return View();
        }

        // GET: /Error/Error500/
        public ActionResult Error500()
        {
            var ex = Server.GetLastError();

            if (ex != null)
                ViewBag.ErrorDescription = ex.Message;

            return View();
        }

    }
}
