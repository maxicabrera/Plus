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
    public class SocialMediaController : BaseController
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
        public ActionResult Create(SocialMediaModel socialMediaModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(socialMediaModel.ThumbnailPath))
                    socialMediaModel.ThumbnailPath = socialMediaModel.ThumbnailPath.Trim();

                ModelState.Clear();
                TryValidateModel(socialMediaModel);
                if (ModelState.IsValid)
                {
                    ModelController.SocialMediaBL.Create(socialMediaModel.Name, socialMediaModel.ThumbnailPath);
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

            return GetCleanSocialMediaView();
        }

        public ActionResult Edit(int id)
        {
            var socialMedia = ModelController.SocialMediaBL.GetById(id);
            if (socialMedia != null)
                ViewData.Model = new SocialMediaModel(socialMedia, Url.Action("Download", "SocialMedia", new { id = id }));

            return View();
        }


        [HttpPost]
        public ActionResult Edit(SocialMediaModel socialMediaModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(socialMediaModel.ThumbnailPath))
                    socialMediaModel.ThumbnailPath = socialMediaModel.ThumbnailPath.Trim();

                ModelState.Clear();
                TryValidateModel(socialMediaModel);
                if (ModelState.IsValid)
                {
                    ModelController.SocialMediaBL.Update(socialMediaModel.IdSocialMedia, socialMediaModel.Name, socialMediaModel.ThumbnailPath);
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

            return GetCleanSocialMediaView();
        }

        public ActionResult BulkDelete(int[] selectedItems)
        {
            try
            {
                if (selectedItems != null && selectedItems.Any())
                {
                    foreach (var id in selectedItems)
                        ModelController.SocialMediaBL.Delete(id);
                    ModelController.SubmitChanges();

                    var confirm = String.Format("{0} {1}(s) have been successfully deleted.", selectedItems.Length, "SocialMedia");
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

            var socialMediaList = new List<SocialMediaModel>();
            int total = 0;

            if (!string.IsNullOrEmpty(filter))
            {
                socialMediaList = SocialMediaModel.ConvertList(ModelController.SocialMediaBL.GetByFilter(filter));

                total = socialMediaList.Count;
                socialMediaList = socialMediaList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            else
            {
                socialMediaList = SocialMediaModel.ConvertList(ModelController.SocialMediaBL.GetAll().ToList());

                total = socialMediaList.Count;
                socialMediaList = socialMediaList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }

            switch (sorterColumn)
            {
                case 1:
                    if (!sorterColumnDir.Equals("desc", StringComparison.OrdinalIgnoreCase))
                        socialMediaList = socialMediaList.OrderBy(sm => sm.Name).ToList();
                    else
                        socialMediaList = socialMediaList.OrderByDescending(sm => sm.Name).ToList();
                    break;
                default:
                    socialMediaList = socialMediaList.OrderBy(sm => sm.Name).ToList();
                    break;
            }

            JsonResult json;

            var result = from sm in socialMediaList
                         select new[] 
                        { 
                            String.Format("<input type=\"checkbox\" [0] onchange=\"toogleMenuButtons('tblSocialMedia');\" name=\"selectedItems\" id=\"selectedItems\" data=\"{0}\" value=\"{1}\" />", sm.Name, sm.IdSocialMedia),
                            String.Format("<a href=\"#addedit\" data-toggle=\"modal\" onclick=\"javascript:editItem({0}); return false;\">{1}</a>",sm.IdSocialMedia,sm.Name),
                            String.Format("<img src=\"{0}\"/>",sm.ThumbnailPath)
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
            var item = ModelController.SocialMediaBL.GetById(id);
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

        private ViewResult GetCleanSocialMediaView()
        {
            ViewResult view = GetCleanView();
            view.ViewName = "Create";
            view.MasterName = "_emptyLayout";

            view.ViewData.Model = new SocialMediaModel();

            return view;
        }

        #endregion

    }
}
