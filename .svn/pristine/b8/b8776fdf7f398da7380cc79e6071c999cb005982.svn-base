using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Security;
using System.Threading;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Data.OleDb;
using System.Globalization;
using Plus54PortfolioRedesign2014.Web.Properties;
using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using Plus54PortfolioRedesign2014.Resources.resources.global;
using Plus54PortfolioRedesign2014.Common;
using Plus54PortfolioRedesign2014.Web.AppCode.Helpers.Progress;
using Plus54PortfolioRedesign2014.Web.Admin.Models;

namespace Plus54PortfolioRedesign2014.Web
{
    public enum ExcelExtencionType
    {
        CSV = 1,
        XLSX = 2,
        XLS = 3
    }

    public abstract class BaseImportController : BaseController
    {
        public string OrderByColumn { get; set; }

        public ActionResult ImportFile(ImportFilesModel model)
        {
            if (ModelState.IsValid)
            {
                model.IdTask = Guid.NewGuid();
                //model.IdCurrentUser = SessionHelper.CurrentUser.IdUser;

                var progressHelper = new ProgressHelper(this.HttpContext.Cache, model.IdTask.Value);
                progressHelper.StartNewTask();

                var thread = new Thread(new ParameterizedThreadStart(DoImport));

                thread.Start(model);

                return Json(model.IdTask.Value);
            }
            else
            {
                return GetJsonErrors(ModelState);
            }
        }

        public virtual void DoImport(object obj)
        {
            //IMPORTANT: inside this method (which runs in other thread) there is no access to session variables            
        }

        public abstract void ProcessRow(DataRow row, float progress, int totalRows, int rowIndex, int errorRows, ProgressHelper progressHelper, ImportFilesModel importModel);

        public ActionResult Progress(Guid idTask)
        {
            try
            {
                var ph = ProgressHelper.GetProgressHelper(this.HttpContext.Cache, idTask);

                if (ph != null)
                {
                    var jsonResult = Json(ph.GetCurrentTask());
                    ph.RaiseFlushedEventHandler();
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                string descEx = ex.ToString();
                return new EmptyResult();
            }

            return new EmptyResult();
        }

        public bool dummyCallback()
        {
            return true;
        }

        void progressHelper_ProgressCompleted(Task currentTask, string importLogFilePath)
        {
            if (!string.IsNullOrEmpty(importLogFilePath))
            {
                var filePath = FileHelper.GetFullPath(importLogFilePath);
                //TODO: if replacement of brake with new line is bad in performance, update the resource file to use directly \r\n instead of br
                System.IO.File.WriteAllText(filePath, currentTask.InfoText.Replace("<br/>", Environment.NewLine));
            }
        }

        protected long GetFileSize(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }

        protected string GetStringValue(System.Data.DataRow row, int index)
        {
            return row[index] != null ? row[index].ToString().Trim() : string.Empty;
        }

        protected int GetIntValue(System.Data.DataRow row, int index)
        {
            return row[index] != null ? int.Parse(row[index].ToString().Trim()) : -1;
        }
    }
}