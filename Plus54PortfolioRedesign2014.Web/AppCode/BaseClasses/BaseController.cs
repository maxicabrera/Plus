using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Security;
using Plus54PortfolioRedesign2014.Common;
using Plus54PortfolioRedesign2014.Web.Controllers;
using Plus54PortfolioRedesign2014.BusinessLogic;
using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using Plus54PortfolioRedesign2014.Entities;

namespace Plus54PortfolioRedesign2014.Web
{
    public class BaseController : System.Web.Mvc.Controller
    {

        protected ModelController ModelController = new ModelController();


        public BaseController() { }


        public JsonResult GetJsonErrors(ModelStateDictionary modelState)
        {
            var errors = GetErrors(modelState);
            if (errors.Count() > 0)
                return GetJsonForErrors(errors);

            return null;
        }


        private JsonResult GetJsonForErrors(Dictionary<string, ModelErrorCollection> errorList)
        {
            var errorMessage = new StringBuilder();
            foreach (var validationItem in errorList)
            {
                foreach (var error in validationItem.Value)
                {
                    if (errorMessage.Length > 0)
                        errorMessage.Append(string.Format("<br/>{0}", error.ErrorMessage));
                    else
                        errorMessage.Append(string.Format("{0}", error.ErrorMessage));
                }
            }
            return Json(new BaseResponse(false, errorMessage.ToString()), JsonRequestBehavior.AllowGet);
        }


        private Dictionary<string, ModelErrorCollection> GetErrors(ModelStateDictionary modelState)
        {
            var errors = modelState.Where(x => x.Value.Errors.Count > 0)
                                        .Select(x => new { x.Key, x.Value.Errors })
                                        .ToDictionary(o => o.Key, o => o.Errors);

            return errors;
        }

        /// <summary>
        /// Clear all fields
        /// </summary>
        /// <returns></returns>
        protected virtual ViewResult GetCleanView()
        {
            ModelState.Clear();
            return View();
        }

        //Login Method
        //protected void Login(AdminUser user)
        //{
        //    if (user != null)
        //    {
        //        SessionHelper.CurrentUser = new UserSession(user);
        //        FormsAuthentication.SetAuthCookie(user.Email, true);
        //    }
        //}

        protected bool IsGuidValid(string guidString)
        {
            Guid guid;
            return Guid.TryParse(guidString, out guid);
        }

        protected string GetServerUrl()
        {
            return string.Concat("http://", Request.Url.Authority, "/");
        }

    }
}