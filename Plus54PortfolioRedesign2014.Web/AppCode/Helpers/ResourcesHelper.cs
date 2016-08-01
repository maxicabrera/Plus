using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Globalization;
using System.Resources;

namespace Plus54PortfolioRedesign2014.Web
{
    public static class ResourcesHelper
    {
        public static string GetMessageFromResource(string resourceId, Type resourceType)
        {
            ResourceManager rm = new ResourceManager(resourceType);

            string value = rm.GetString(resourceId, System.Threading.Thread.CurrentThread.CurrentCulture);
            return value ?? resourceId;
        }
    }
    public class DatatablesLanguageSettings
    {
        public string sProcessing { get; set; }
        public string sLengthMenu { get; set; }
        public string sZeroRecords { get; set; }
        public string sEmptyTable { get; set; }
        public string sInfo { get; set; }
        public string sInfoEmpty { get; set; }
        public string sInfoFiltered { get; set; }
        public string sInfoPostFix { get; set; }
        public string sSearch { get; set; }
        public string sUrl { get; set; }
        public DatatablesLanguagePaginateSettings oPaginate { get; set; }
        public string sInfoThousands { get; set; }
        public string sLoadingRecords { get; set; }
        public string fnInfoCallback { get; set; }
        public DatatablesLanguageAriaSettings oAria { get; set; }
        public DatatablesLanguageSettings()
        {
            oAria = new DatatablesLanguageAriaSettings();
            oPaginate = new DatatablesLanguagePaginateSettings();
        }
    }

    public class DatatablesLanguagePaginateSettings
    {
        public string sFirst { get; set; }
        public string sLast { get; set; }
        public string sNext { get; set; }
        public string sPrevious { get; set; }
    }

    public class DatatablesLanguageAriaSettings
    {
        public string sSortAscending { get; set; }
        public string sSortDescending { get; set; }
    }
}