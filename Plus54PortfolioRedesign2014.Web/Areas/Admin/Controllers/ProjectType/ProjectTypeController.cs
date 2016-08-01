using Plus54PortfolioRedesign2014.Web.Admin.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;

namespace Plus54PortfolioRedesign2014.Web.Areas.Admin.Controllers
{
    public class ProjectTypeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProjectTypeModel projectTypeModel)
        {
            try
            {
                ModelState.Clear();
                TryValidateModel(projectTypeModel);
                if (ModelState.IsValid)
                {
                    ModelController.ProjectTypeTagBL.Create(projectTypeModel.Name);
                    ModelController.SubmitChanges();
                }
                else
                {
                    var jsonError = GetJsonErrors(ModelState);
                    if (jsonError != null)
                        return jsonError;

                    return Json(new BaseResponse(false, "Unknown error"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("{0}. {1}", "Unknown error", ex.ToString());
                return Json(new BaseResponse(false, message), JsonRequestBehavior.AllowGet);
            }

            return GetCleanProjectTypeView();
        }

        public ActionResult Edit(int id)
        {
            var projectType = ModelController.ProjectTypeTagBL.GetById(id);
            if (projectType != null)
                ViewData.Model = new ProjectTypeModel(projectType);

            return View();
        }
        
        [HttpPost]
        public ActionResult Edit(ProjectTypeModel projectTypeModel)
        {
            try
            {
                ModelState.Clear();
                TryValidateModel(projectTypeModel);
                if (ModelState.IsValid)
                {
                    ModelController.ProjectTypeTagBL.Update(projectTypeModel.IdProjectTypeTag, projectTypeModel.Name);
                    ModelController.SubmitChanges();
                }
                else
                {
                    var jsonError = GetJsonErrors(ModelState);
                    if (jsonError != null)
                        return jsonError;

                    return Json(new BaseResponse(false, "Unknown error"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("{0}. {1}", "Unknown error", ex.ToString());
                return Json(new BaseResponse(false, message), JsonRequestBehavior.AllowGet);
            }

            return GetCleanProjectTypeView();
        }

        public ActionResult BulkDelete(int[] selectedItems)
        {
            try
            {
                if (selectedItems != null && selectedItems.Any())
                {
                    foreach (var id in selectedItems)
                        ModelController.ProjectTypeTagBL.Delete(id);
                    ModelController.SubmitChanges();

                    var confirm = String.Format("{0} {1}(s) have been successfully deleted.", selectedItems.Length, "Project Type");
                    return Json(new BaseResponse(true, confirm, String.Empty, null), JsonRequestBehavior.AllowGet);
                }

                return Json(new BaseResponse(false, "No items selected", String.Empty, null), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("{0}. {1}", "Unknown Error", ex.ToString());
                return Json(new BaseResponse(false, message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAll(string sEcho, int iDisplayStart, int iDisplayLength)
        {
            var filter = Convert.ToString(Request["sSearch"], CultureInfo.CurrentCulture).ToLower(CultureInfo.CurrentCulture);
            var sorterColumn = Convert.ToInt32(Request["iSortCol_0"]);
            var sorterColumnDir = Convert.ToString(Request["sSortDir_0"], CultureInfo.InvariantCulture);

            var projectTypeList = new List<ProjectTypeModel>();
            int total = 0;

            if (!string.IsNullOrEmpty(filter))
            {
                projectTypeList = ProjectTypeModel.ConvertList(ModelController.ProjectTypeTagBL.GetByFilter(filter));

                total = projectTypeList.Count;
                projectTypeList = projectTypeList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            else
            {
                projectTypeList = ProjectTypeModel.ConvertList(ModelController.ProjectTypeTagBL.GetAll().ToList());

                total = projectTypeList.Count;
                projectTypeList = projectTypeList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }

            switch (sorterColumn)
            {
                case 1:
                    if (!sorterColumnDir.Equals("desc", StringComparison.OrdinalIgnoreCase))
                        projectTypeList = projectTypeList.OrderBy(pt => pt.Name).ToList();
                    else
                        projectTypeList = projectTypeList.OrderByDescending(pt => pt.Name).ToList();
                    break;
                default:
                    projectTypeList = projectTypeList.OrderBy(pt => pt.Name).ToList();
                    break;
            }

            JsonResult json;

            var result = from pt in projectTypeList
                         select new[] 
                        { 
                            String.Format("<input type=\"checkbox\" [0] onchange=\"toogleMenuButtons('tblProjectTypes');\" name=\"selectedItems\" id=\"selectedItems\" data=\"{0}\" value=\"{1}\" />", pt.Name, pt.IdProjectTypeTag),
                            String.Format("<a href=\"#addedit\" data-toggle=\"modal\" onclick=\"javascript:editItem({0}); return false;\">{1}</a>",pt.IdProjectTypeTag,pt.Name),
                        };

            json = Json(new
            {
                sEcho = sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = result
            }, JsonRequestBehavior.AllowGet);

            return json;
        }

        #region Private Methods

        private ViewResult GetCleanProjectTypeView()
        {
            ViewResult view = GetCleanView();
            view.ViewName = "Create";
            view.MasterName = "_emptyLayout";

            view.ViewData.Model = new ProjectTypeModel();

            return view;
        }

        #endregion
    }
}
