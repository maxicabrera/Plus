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
    public class TechnologyController : BaseController
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
        public ActionResult Create(TechnologyModel technologyModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(technologyModel.ThumbnailPath))
                    technologyModel.ThumbnailPath = technologyModel.ThumbnailPath.Trim();

                ModelState.Clear();
                TryValidateModel(technologyModel);
                if (ModelState.IsValid)
                {
                    ModelController.TechnologyBL.Create(technologyModel.Name, technologyModel.ThumbnailPath);
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

            return GetCleanTechnologyView();
        }

        public ActionResult Edit(int id)
        {
            var technology = ModelController.TechnologyBL.GetById(id);
            if (technology != null)
                ViewData.Model = new TechnologyModel(technology, Url.Action("Download", "Technology", new { id = id }));

            return View();
        }


        [HttpPost]
        public ActionResult Edit(TechnologyModel technologyModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(technologyModel.ThumbnailPath))
                    technologyModel.ThumbnailPath = technologyModel.ThumbnailPath.Trim();

                ModelState.Clear();
                TryValidateModel(technologyModel);
                if (ModelState.IsValid)
                {
                    ModelController.TechnologyBL.Update(technologyModel.IdTechnology, technologyModel.Name, technologyModel.ThumbnailPath);
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

            return GetCleanTechnologyView();
        }

        public ActionResult BulkDelete(int[] selectedItems)
        {
            try
            {
                if (selectedItems != null && selectedItems.Any())
                {
                    foreach (var id in selectedItems)
                        ModelController.TechnologyBL.Delete(id);
                    ModelController.SubmitChanges();

                    var confirm = String.Format("{0} {1}(s) have been successfully deleted.", selectedItems.Length, "Technology");
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

            var technologiesList = new List<TechnologyModel>();
            int total = 0;

            if (!string.IsNullOrEmpty(filter))
            {
                technologiesList = TechnologyModel.ConvertList(ModelController.TechnologyBL.GetByFilter(filter));

                total = technologiesList.Count;
                technologiesList = technologiesList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            else
            {
                technologiesList = TechnologyModel.ConvertList(ModelController.TechnologyBL.GetAll().ToList());

                total = technologiesList.Count;
                technologiesList = technologiesList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }

            switch (sorterColumn)
            {
                case 1:
                    if (!sorterColumnDir.Equals("desc", StringComparison.OrdinalIgnoreCase))
                        technologiesList = technologiesList.OrderBy(t => t.Name).ToList();
                    else
                        technologiesList = technologiesList.OrderByDescending(t => t.Name).ToList();
                    break;
                default:
                    technologiesList = technologiesList.OrderBy(t => t.Name).ToList();
                    break;
            }

            JsonResult json;

            var result = from t in technologiesList
                         select new[] 
                        { 
                            String.Format("<input type=\"checkbox\" [0] onchange=\"toogleMenuButtons('tblTechnologies');\" name=\"selectedItems\" id=\"selectedItems\" data=\"{0}\" value=\"{1}\" />", t.Name, t.IdTechnology),
                            String.Format("<a href=\"#addedit\" data-toggle=\"modal\" onclick=\"javascript:editItem({0}); return false;\">{1}</a>",t.IdTechnology,t.Name),
                            String.Format("<img src=\"{0}\"/>",t.ThumbnailPath)
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
            var item = ModelController.TechnologyBL.GetById(id);
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

        private ViewResult GetCleanTechnologyView()
        {
            ViewResult view = GetCleanView();
            view.ViewName = "Create";
            view.MasterName = "_emptyLayout";

            view.ViewData.Model = new TechnologyModel();

            return view;
        }

        #endregion
    }
}
