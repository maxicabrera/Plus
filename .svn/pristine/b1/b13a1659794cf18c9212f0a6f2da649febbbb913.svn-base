using Plus54PortfolioRedesign2014.Web.Admin.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Plus54PortfolioRedesign2014.Web.Areas.Admin.Controllers
{

    public class ProjectController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult List()
        {
            AddDataToViewBag();
            return View();
        }
        
        public ActionResult Create()
        {
            AddDataToViewBag();
            CleanUploaderSession();
            return View();
        }
      
        [HttpPost]
        public async Task<ActionResult> Create(ProjectModel projectModel)
        {
            await UploadProjectSiteLogo(string.Empty);

            //waiting logo and slider uploader tasks
            while (Session[PROJECT_SLIDER_IMAGES_FINISHED_KEY] == null || Session[PROJECT_SITE_LOGO_FINISHED_KEY] == null)
                await Task.Delay(2000);

            try
            {
                //retrieve site logo path from session
                if (Session[PROJECT_SITE_LOGO_TEMP_KEY] != null && !string.IsNullOrEmpty(Session[PROJECT_SITE_LOGO_TEMP_KEY].ToString()))
                    projectModel.SiteLogoPath = Session[PROJECT_SITE_LOGO_TEMP_KEY].ToString().Trim();
                
                //retrieve slider image paths from session
                projectModel.SliderImages = new List<string>();
                if (Session[PROJECT_SLIDER_IMAGES_TEMP_KEY] != null && !string.IsNullOrEmpty(Session[PROJECT_SLIDER_IMAGES_TEMP_KEY].ToString()))
                {
                    var sliderImages = Session[PROJECT_SLIDER_IMAGES_TEMP_KEY].ToString().Trim().Split('|');
                    foreach(var sliderImage in sliderImages)
                        if(!string.IsNullOrEmpty(sliderImage))
                            projectModel.SliderImages.Add(sliderImage);
                }

                if (!string.IsNullOrEmpty(projectModel.ThumbnailPath))
                    projectModel.ThumbnailPath = projectModel.ThumbnailPath.Trim();

                CleanUploaderSession();

                ModelState.Clear();
                TryValidateModel(projectModel);
                if (ModelState.IsValid)
                {
                    ModelController.ProjectBL.Create(projectModel.Name, projectModel.Overview, projectModel.Url, projectModel.ClientName, projectModel.PartnerName, projectModel.Year, projectModel.ThumbnailPath, projectModel.SiteLogoPath, projectModel.SliderImages, projectModel.ProjectType, projectModel.Category, projectModel.Technologies, projectModel.SocialMedia, projectModel.ScreenSupport);
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

            return GetCleanProjectView();
        }
      
        public ActionResult Edit(int id)
        {
            var project = ModelController.ProjectBL.GetById(id);
            if (project != null)
                ViewData.Model = new ProjectModel(project);

            AddDataToViewBag();
            CleanUploaderSession();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProjectModel projectModel, string deletedImages)
        {
            //PORTFOLIO-82
            await UploadProjectSiteLogo(string.Empty);

            //waiting logo and slider uploader tasks
            while (Session[PROJECT_SLIDER_IMAGES_FINISHED_KEY] == null || Session[PROJECT_SITE_LOGO_FINISHED_KEY] == null)
                await Task.Delay(2000);

            try
            {
                //retrieve site logo path from session
                if (Session[PROJECT_SITE_LOGO_TEMP_KEY] != null && !string.IsNullOrEmpty(Session[PROJECT_SITE_LOGO_TEMP_KEY].ToString()))
                    projectModel.SiteLogoPath = Session[PROJECT_SITE_LOGO_TEMP_KEY].ToString().Trim();

                //retrieve slider image paths from session
                projectModel.SliderImages = new List<string>();
                if (Session[PROJECT_SLIDER_IMAGES_TEMP_KEY] != null && !string.IsNullOrEmpty(Session[PROJECT_SLIDER_IMAGES_TEMP_KEY].ToString()))
                {
                    var sliderImages = Session[PROJECT_SLIDER_IMAGES_TEMP_KEY].ToString().Trim().Split('|');
                    foreach (var sliderImage in sliderImages)
                        if (!string.IsNullOrEmpty(sliderImage))
                            projectModel.SliderImages.Add(sliderImage);
                }

                projectModel.DeletedSliderImages = new List<string>();
                var deletedSliderImages = deletedImages.Trim().Split('|');
                foreach (var deletedSliderImage in deletedSliderImages)
                    if (!string.IsNullOrEmpty(deletedSliderImage))
                        projectModel.DeletedSliderImages.Add(deletedSliderImage);

                CleanUploaderSession();

                ModelState.Clear();
                TryValidateModel(projectModel);
                if (ModelState.IsValid)
                {
                    ModelController.ProjectBL.Update(projectModel.IdProject, projectModel.Name, projectModel.Overview, projectModel.Url, projectModel.ClientName, projectModel.PartnerName, projectModel.Year, projectModel.ThumbnailPath, projectModel.SiteLogoPath, projectModel.SliderImages, projectModel.DeletedSliderImages, projectModel.ProjectType, projectModel.Category, projectModel.Technologies, projectModel.SocialMedia, projectModel.ScreenSupport);
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

            return GetCleanProjectView();
        }

        public ActionResult BulkDelete(int[] selectedItems)
        {
            try
            {
                if (selectedItems != null && selectedItems.Any())
                {
                    foreach (var id in selectedItems)
                        ModelController.ProjectBL.Delete(id);
                    ModelController.SubmitChanges();

                    var confirm = String.Format("{0} {1}(s) have been successfully deleted.", selectedItems.Length, "Project");
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

        public JsonResult GetAll(string sEcho, int iDisplayStart, int iDisplayLength, int client, int partner, int category, int projectType, int technology, int socialMedia)
        {
            var filter = Convert.ToString(Request["sSearch"], CultureInfo.CurrentCulture).ToLower(CultureInfo.CurrentCulture);
            var sorterColumn = Convert.ToInt32(Request["iSortCol_0"]);
            var sorterColumnDir = Convert.ToString(Request["sSortDir_0"], CultureInfo.InvariantCulture);

            var projectsList = new List<ProjectModel>();
            int total = 0;

            if (!string.IsNullOrEmpty(filter) || client > 0 || partner > 0 || category > 0 || projectType > 0 || technology > 0 || socialMedia > 0)
            {
                projectsList = ProjectModel.ConvertList(ModelController.ProjectBL.GetByNameAndData(filter, client, partner, category, projectType, technology, socialMedia));

                total = projectsList.Count;
                projectsList = projectsList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            else
            {
                projectsList = ProjectModel.ConvertList(ModelController.ProjectBL.GetAll().ToList());

                total = projectsList.Count;
                projectsList = projectsList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }

            switch (sorterColumn)
            {
                case 1:
                    if (!sorterColumnDir.Equals("desc", StringComparison.OrdinalIgnoreCase))
                        projectsList = projectsList.OrderBy(p => p.Name).ToList();
                    else
                        projectsList = projectsList.OrderByDescending(p => p.Name).ToList();
                    break;
                case 2:
                    if (!sorterColumnDir.Equals("desc", StringComparison.OrdinalIgnoreCase))
                        projectsList = projectsList.OrderBy(p => p.ClientName).ToList();
                    else
                        projectsList = projectsList.OrderByDescending(p => p.ClientName).ToList();
                    break;
                case 3:
                    if (!sorterColumnDir.Equals("desc", StringComparison.OrdinalIgnoreCase))
                        projectsList = projectsList.OrderBy(p => p.PartnerName).ToList();
                    else
                        projectsList = projectsList.OrderByDescending(p => p.PartnerName).ToList();
                    break;
                default:
                    projectsList = projectsList.OrderBy(p => p.Name).ToList();
                    break;
            }

            JsonResult json;

            var result = from p in projectsList
                          select new [] 
                        { 
                            String.Format("<input type=\"checkbox\" [0] onchange=\"toogleMenuButtons('tblProjects');\" name=\"selectedItems\" id=\"selectedItems\" data=\"{0}\" value=\"{1}\" />", p.Name, p.IdProject),
                            String.Format("<a href=\"#addedit\" data-toggle=\"modal\" onclick=\"javascript:editItem({0}); return false;\">{1}</a>",p.IdProject,p.Name),
                            p.ClientName,
                            p.PartnerName,
                            string.Join(",", p.CategoryName),
                            string.Join(",", p.ProjectTypeNames),                            
                            string.Join(",", p.TechnologyNames),
                            string.Join(",", p.SocialMediaNames)                            
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

        #region Async Uploader Methods

        private const string PROJECT_SITE_LOGO_TEMP_KEY = "SITE_LOGO_PATH";
        private const string PROJECT_SLIDER_IMAGES_TEMP_KEY = "SLIDER_IMAGES_PATH";

        private const string PROJECT_SLIDER_IMAGES_UPLOADED_KEY = "UPLOADED_SLIDER_IMAGES";

        private const string PROJECT_SLIDER_IMAGES_FINISHED_KEY = "FINISHED_SLIDER_IMAGES";
        private const string PROJECT_SITE_LOGO_FINISHED_KEY = "FINISHED_SITE_LOGO";

        private void CleanUploaderSession()
        {
            Session.Remove(PROJECT_SITE_LOGO_TEMP_KEY);
            Session.Remove(PROJECT_SLIDER_IMAGES_TEMP_KEY);
            Session.Remove(PROJECT_SLIDER_IMAGES_UPLOADED_KEY);
            Session.Remove(PROJECT_SLIDER_IMAGES_FINISHED_KEY);
            Session.Remove(PROJECT_SITE_LOGO_FINISHED_KEY);
        }

        [HttpPost]
        public async Task<ActionResult> UploadProjectSiteLogo(string siteLogoPath)
        {
            if (!string.IsNullOrEmpty(siteLogoPath))
                Session[PROJECT_SITE_LOGO_TEMP_KEY] = siteLogoPath;

            Session[PROJECT_SITE_LOGO_FINISHED_KEY] = true;

            return Json(new BaseResponse(true), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UploadProjectSliderImages(string sliderImages, int sliderImagesCount)
        {
            if (Session[PROJECT_SLIDER_IMAGES_UPLOADED_KEY] == null)
                Session[PROJECT_SLIDER_IMAGES_UPLOADED_KEY] = 1;
            else
                Session[PROJECT_SLIDER_IMAGES_UPLOADED_KEY] = int.Parse(Session[PROJECT_SLIDER_IMAGES_UPLOADED_KEY].ToString()) + 1;

            if (!string.IsNullOrEmpty(sliderImages) && sliderImagesCount > 0)
            {
                if (Session[PROJECT_SLIDER_IMAGES_TEMP_KEY] != null)
                    Session[PROJECT_SLIDER_IMAGES_TEMP_KEY] = string.Format("{0}|{1}", Session[PROJECT_SLIDER_IMAGES_TEMP_KEY].ToString().Trim(), sliderImages);
                else
                    Session[PROJECT_SLIDER_IMAGES_TEMP_KEY] = sliderImages;
            }

            if (int.Parse(Session[PROJECT_SLIDER_IMAGES_UPLOADED_KEY].ToString()) >= sliderImagesCount)
                Session[PROJECT_SLIDER_IMAGES_FINISHED_KEY] = true;

            return Json(new BaseResponse(true), JsonRequestBehavior.AllowGet);
        }

        #endregion    

        #region Image Methods

        public ActionResult Download(int id)
        {
            var item = ModelController.ProjectBL.GetById(id);
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

        private ViewResult GetCleanProjectView()
        {
            ViewResult view = GetCleanView();
            view.ViewName = "Create";
            view.MasterName = "_emptyLayout";

            view.ViewData.Model = new ProjectModel();

            return view;
        }

        private void AddDataToViewBag()
        {
            AddYearsToViewBag();
            AddClientsToViewBag();
            AddPartnersToViewBag();
            AddProjectCategoriesToViewBag();
            AddTechnologiesToViewBag();
            AddSocialMediaToViewBag();
            AddProjectTypeToViewBag();
            AddScreenSupportToViewBag();
        }

        private void AddYearsToViewBag()
        {
            ViewBag.years = Enumerable.Range(2000, DateTime.Now.Year - 2000 + 1).Select(year => new SelectListItem() { Text = year.ToString(), Value = year.ToString(), Selected = false }).OrderByDescending(year => year.Text);
        }

        private void AddClientsToViewBag()
        {
            ViewBag.clients = ClientModel.ConvertList(ModelController.ClientBL.GetAll().OrderBy(c => c.Name).ToList());
        }

        private void AddPartnersToViewBag()
        {
            ViewBag.partners = PartnerModel.ConvertList(ModelController.PartnerBL.GetAll().OrderBy(p => p.Name).ToList());
        }

        private void AddProjectCategoriesToViewBag()
        {
            ViewBag.categories = ProjectCategoryModel.ConvertList(ModelController.ProjectCategoryBL.GetAll().OrderBy(c => c.Name).ToList());
        }
        private void AddScreenSupportToViewBag()
        {
            ViewBag.screensupport = ScreenSupportModel.ConvertList(ModelController.ScreenSupportBL.GetAll().OrderBy(c => c.Name).ToList());
        }

        private void AddTechnologiesToViewBag()
        {
            ViewBag.technologies = TechnologyModel.ConvertList(ModelController.TechnologyBL.GetAll().OrderBy(c => c.Name).ToList());
        }

        private void AddSocialMediaToViewBag()
        {
            ViewBag.socialMedia = SocialMediaModel.ConvertList(ModelController.SocialMediaBL.GetAll().OrderBy(c => c.Name).ToList());
        }

        private void AddProjectTypeToViewBag()
        {
            ViewBag.projectType = ProjectTypeModel.ConvertList(ModelController.ProjectTypeTagBL.GetAll().OrderBy(c => c.Name).ToList());
        }

        #endregion
    }
}
