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
    public class ScreenSupportController : BaseController
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
        public ActionResult Create(ScreenSupportModel screenSupportModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(screenSupportModel.ThumbnailPath))
                    screenSupportModel.ThumbnailPath = screenSupportModel.ThumbnailPath.Trim();

                ModelState.Clear();
                TryValidateModel(screenSupportModel);
                if (ModelState.IsValid)
                {
                    ModelController.ScreenSupportBL.Create(screenSupportModel.Name, screenSupportModel.ThumbnailPath);
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

            return GetCleanScreenSupportView();
        }

        public ActionResult Edit(int id)
        {
            var screenSupport = ModelController.ScreenSupportBL.GetById(id);
            if (screenSupport != null)
                ViewData.Model = new ScreenSupportModel(screenSupport, Url.Action("Download", "ScreenSupport", new { id = id }));

            return View();
        }


        [HttpPost]
        public ActionResult Edit(ScreenSupportModel screenSupportModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(screenSupportModel.ThumbnailPath))
                    screenSupportModel.ThumbnailPath = screenSupportModel.ThumbnailPath.Trim();

                ModelState.Clear();
                TryValidateModel(screenSupportModel);
                if (ModelState.IsValid)
                {
                    ModelController.ScreenSupportBL.Update(screenSupportModel.IdScreenSupport, screenSupportModel.Name, screenSupportModel.ThumbnailPath);
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

            return GetCleanScreenSupportView();
        }

        public ActionResult BulkDelete(int[] selectedItems)
        {
            try
            {
                if (selectedItems != null && selectedItems.Any())
                {
                    foreach (var id in selectedItems)
                        ModelController.ScreenSupportBL.Delete(id);
                    ModelController.SubmitChanges();

                    var confirm = String.Format("{0} {1}(s) have been successfully deleted.", selectedItems.Length, "Screen Support");
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

            var screenSupportList = new List<ScreenSupportModel>();
            int total = 0;

            if (!string.IsNullOrEmpty(filter))
            {
                screenSupportList = ScreenSupportModel.ConvertList(ModelController.ScreenSupportBL.GetByFilter(filter));

                total = screenSupportList.Count;
                screenSupportList = screenSupportList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            else
            {
                screenSupportList = ScreenSupportModel.ConvertList(ModelController.ScreenSupportBL.GetAll().ToList());

                total = screenSupportList.Count;
                screenSupportList = screenSupportList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }

            switch (sorterColumn)
            {
                case 1:
                    if (!sorterColumnDir.Equals("desc", StringComparison.OrdinalIgnoreCase))
                        screenSupportList = screenSupportList.OrderBy(pc => pc.Name).ToList();
                    else
                        screenSupportList = screenSupportList.OrderByDescending(pc => pc.Name).ToList();
                    break;
                default:
                    screenSupportList = screenSupportList.OrderBy(pc => pc.Name).ToList();
                    break;
            }

            JsonResult json;

            var result = from pc in screenSupportList
                         select new[] 
                        { 
                            String.Format("<input type=\"checkbox\" [0] onchange=\"toogleMenuButtons('tblScreenSupport');\" name=\"selectedItems\" id=\"selectedItems\" data=\"{0}\" value=\"{1}\" />", pc.Name, pc.IdScreenSupport),
                            String.Format("<a href=\"#addedit\" data-toggle=\"modal\" onclick=\"javascript:editItem({0}); return false;\">{1}</a>",pc.IdScreenSupport,pc.Name),
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
            var item = ModelController.ScreenSupportBL.GetById(id);
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

        private ViewResult GetCleanScreenSupportView()
        {
            ViewResult view = GetCleanView();
            view.ViewName = "Create";
            view.MasterName = "_emptyLayout";

            view.ViewData.Model = new ScreenSupportModel();

            return view;
        }

        #endregion
    }
}
