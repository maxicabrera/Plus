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
    public class ProjectCategoryController : BaseController
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
        public ActionResult Create(ProjectCategoryModel projectCategoryModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(projectCategoryModel.ThumbnailPath))
                    projectCategoryModel.ThumbnailPath = projectCategoryModel.ThumbnailPath.Trim();

                ModelState.Clear();
                TryValidateModel(projectCategoryModel);
                if (ModelState.IsValid)
                {
                    ModelController.ProjectCategoryBL.Create(projectCategoryModel.Name, projectCategoryModel.ThumbnailPath);
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

            return GetCleanProjectCategoryView();
        }

        public ActionResult Edit(int id)
        {
            var projectCategory = ModelController.ProjectCategoryBL.GetById(id);
            if (projectCategory != null)
                ViewData.Model = new ProjectCategoryModel(projectCategory, Url.Action("Download", "ProjectCategory", new { id = id }));

            return View();
        }


        [HttpPost]
        public ActionResult Edit(ProjectCategoryModel projectCategoryModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(projectCategoryModel.ThumbnailPath))
                    projectCategoryModel.ThumbnailPath = projectCategoryModel.ThumbnailPath.Trim();

                ModelState.Clear();
                TryValidateModel(projectCategoryModel);
                if (ModelState.IsValid)
                {
                    ModelController.ProjectCategoryBL.Update(projectCategoryModel.IdProjectCategory, projectCategoryModel.Name, projectCategoryModel.ThumbnailPath);
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

            return GetCleanProjectCategoryView();
        }

        public ActionResult BulkDelete(int[] selectedItems)
        {
            try
            {
                if (selectedItems != null && selectedItems.Any())
                {
                    foreach (var id in selectedItems)
                        ModelController.ProjectCategoryBL.Delete(id);
                    ModelController.SubmitChanges();

                    var confirm = String.Format("{0} {1}(s) have been successfully deleted.", selectedItems.Length, "Project Category");
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

            var projectCategoriesList = new List<ProjectCategoryModel>();
            int total = 0;

            if (!string.IsNullOrEmpty(filter))
            {
                projectCategoriesList = ProjectCategoryModel.ConvertList(ModelController.ProjectCategoryBL.GetByFilter(filter));

                total = projectCategoriesList.Count;
                projectCategoriesList = projectCategoriesList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            else
            {
                projectCategoriesList = ProjectCategoryModel.ConvertList(ModelController.ProjectCategoryBL.GetAll().ToList());

                total = projectCategoriesList.Count;
                projectCategoriesList = projectCategoriesList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }

            switch (sorterColumn)
            {
                case 1:
                    if (!sorterColumnDir.Equals("desc", StringComparison.OrdinalIgnoreCase))
                        projectCategoriesList = projectCategoriesList.OrderBy(pc => pc.Name).ToList();
                    else
                        projectCategoriesList = projectCategoriesList.OrderByDescending(pc => pc.Name).ToList();
                    break;
                default:
                    projectCategoriesList = projectCategoriesList.OrderBy(pc => pc.Name).ToList();
                    break;
            }

            JsonResult json;

            var result = from pc in projectCategoriesList
                         select new[] 
                        { 
                            String.Format("<input type=\"checkbox\" [0] onchange=\"toogleMenuButtons('tblProjectCategories');\" name=\"selectedItems\" id=\"selectedItems\" data=\"{0}\" value=\"{1}\" />", pc.Name, pc.IdProjectCategory),
                            String.Format("<a href=\"#addedit\" data-toggle=\"modal\" onclick=\"javascript:editItem({0}); return false;\">{1}</a>",pc.IdProjectCategory,pc.Name),
                            String.Format("<img src=\"{0}\"/>",pc.ThumbnailPath)
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

        #region Image Methods

        public ActionResult Download(int id)
        {
            var item = ModelController.ProjectCategoryBL.GetById(id);
            if (item != null)
            {
                string link = item.Thumbnail.Path;
                if (!string.IsNullOrEmpty(link))
                {
                    var filePath = FileHelper.GetFullPath(link);
                    if (System.IO.File.Exists(filePath))
                    {
                        byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                        string fileName = FileHelper.GetFileName(filePath);
                        return File(bytes,
                                     "application/octet-stream",
                                      fileName);
                    }
                }
            }
            ModelState.AddModelError("File", "File Not Found");
            var jsonError = GetJsonErrors(ModelState);
            if (jsonError != null)
                return jsonError;

            return Json(new BaseResponse(false, "Unknown error"), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Private Methods

        private ViewResult GetCleanProjectCategoryView()
        {
            ViewResult view = GetCleanView();
            view.ViewName = "Create";
            view.MasterName = "_emptyLayout";

            view.ViewData.Model = new ProjectCategoryModel();

            return view;
        }

        #endregion
    }
}
