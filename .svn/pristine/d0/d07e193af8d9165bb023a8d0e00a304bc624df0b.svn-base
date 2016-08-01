using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
//using Plus54PortfolioRedesign2014.Resources.resources.models;

namespace Plus54PortfolioRedesign2014.Web.Admin.Models
{
    public class ImportFilesModel
    {
        //[LocalizedDisplayName("SheetName", NameResourceType = typeof(ImportFilesModel_res))]
        public string SheetName { get; set; }


        #region File

        //[LocalizedDisplayName("Link", NameResourceType = typeof(ImportFilesModel_res))]
        //[LocalizedRequired(ErrorMessageResourceName = "Link_ErrorMessage", ErrorMessageResourceType = typeof(ImportFilesModel_res))]    //  TO TEST because there isn't working well
        public string Link { get; set; }

        //[LocalizedDisplayName("UpdateTextImport", NameResourceType = typeof(ImportFilesModel_res))]
        public bool UpdateTextImport { get; set; }

        public Guid? IdTask { get; set; }
        public int IdCurrentUser { get; set; }
        public string Url { get; set; }
        public string Auth { get; set; }
        //[LocalizedDisplayName("ReplaceExactTarget", NameResourceType = typeof(ImportFilesModel_res))]
        public bool ReplaceExactTarget { get; set; }

        public long TotalItems { get; set; }
        public string ImportLogFilePath { get; set; }


        #endregion

        public ImportFilesModel()
        {

        }
    }
}