using Plus54PortfolioRedesign2014.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Plus54PortfolioRedesign2014.Web.Controllers
{
    public class HomeController : BaseController
    {

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (TempData["loginError"]!=null)
                ViewBag.loginError = TempData["loginError"];

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string user, string password)
        {
            TempData.Remove("loginError");
            if (ModelController.UserBL.ValidatePlayerLogOn(user, password))
            {
                SessionHelper.CurrentUser = new UserSession();
                FormsAuthentication.SetAuthCookie(user, true);

                return RedirectToAction("Index");
            }
            else
            {
                TempData["loginError"] = true;
                return RedirectToAction("Login");
            }
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            SessionHelper.Logout();           
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        //[CustomAuthorizeAttribute]
        //public ActionResult SplashScreen()
        //{
        //    TODO: Pending to be included  
        //    return View();  
        //}

        [CustomAuthorizeAttribute]
        public ActionResult Index()
        {
            AddDataToViewBag();
            return View();
        }

        #region Private Methods

        private void AddDataToViewBag()
        {
            AddTechnologiesToViewBag();
            AddProjectTypeToViewBag();
        }

        private void AddTechnologiesToViewBag()
        {
            ViewBag.technologies = TechnologyModel.ConvertList(ModelController.TechnologyBL.GetAll().OrderBy(c => c.Name).ToList());
        }

        private void AddProjectTypeToViewBag()
        {
            ViewBag.projectType = ProjectTypeModel.ConvertList(ModelController.ProjectTypeTagBL.GetAll().OrderBy(c => c.Name).ToList());
        }

        #endregion
    }
}
