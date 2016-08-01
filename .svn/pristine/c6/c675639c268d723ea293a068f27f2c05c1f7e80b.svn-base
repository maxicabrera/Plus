using Plus54PortfolioRedesign2014.Web.AppCode.BaseClasses;
using Plus54PortfolioRedesign2014.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plus54PortfolioRedesign2014.Web.Controllers
{
    public class ProjectAjaxController : BaseController
    {
        [HttpGet]
        [CustomAuthorizeAttribute]
        public JsonResult Get(int idProject)
        {
            var project = new ProjectDetailModel(ModelController.ProjectBL.GetById(idProject));

            return Json(project, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [CustomAuthorizeAttribute]
        public JsonResult GetAll(int pageSize, int pageNumber)
        {
            var projectsList = ModelController.ProjectBL.GetAllOrderedByCreatedDate();
            var total = projectsList.Count;

            if (pageNumber > 0)
            {
                if (pageNumber == 1)
                    return Json(new ProjectBaseResponse<ProjectModel>(pageNumber, total, ProjectModel.ConvertList(projectsList.Take(pageSize).ToList())), JsonRequestBehavior.AllowGet);
                else
                    return Json(new ProjectBaseResponse<ProjectModel>(pageNumber, total, ProjectModel.ConvertList(projectsList.Skip(pageSize * pageNumber).Take(pageSize).ToList())), JsonRequestBehavior.AllowGet);
            }

            return Json(new ProjectBaseResponse<ProjectModel>(0, 0, null), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [CustomAuthorizeAttribute]
        public JsonResult GetAllByFilterAndData(int pageSize, int pageNumber, string keywords, int[] technologies, int[] projectTypes)
        {
            var projectsList = ModelController.ProjectBL.GetAllByFilterAndData(keywords, technologies, projectTypes);
            var total = projectsList.Count;

            if (pageNumber > 0)
            {
                List<ProjectModel> list = new List<ProjectModel>();
                if (pageNumber == 1)
                    list = ProjectModel.ConvertList(projectsList.Take(pageSize).ToList());
                else
                    list = ProjectModel.ConvertList(projectsList.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList());

                return Json(new ProjectBaseResponse<ProjectModel>(pageNumber, total, list), JsonRequestBehavior.AllowGet);
            }

            return Json(new ProjectBaseResponse<ProjectDetailModel>(0, 0, null), JsonRequestBehavior.AllowGet);
        }

    }
}
