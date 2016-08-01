using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Plus54PortfolioRedesign2014.Web.Properties;
using System.Web.Caching;

namespace Plus54PortfolioRedesign2014.Web.AppCode.Helpers
{
    public class ProjectSvnHelper
    {
        private static string SvnFilePath { get; set; }

        static ProjectSvnHelper()
        {
            string fileName = Settings.Default.SvnRevisionFilePath;
            string filePath = Path.GetFullPath(HttpContext.Current.Server.MapPath("~") + fileName);

            if (File.Exists(filePath))
            {
                SvnFilePath = filePath;
            }
        }

        public static string GetCmsReleaseVersion()
        {
            if ((HttpContext.Current.Cache["CmsSvnVersion"] == null))
            {
                string fullReleaseVersion = string.Empty;
                string revisionVersion = GetRevisionNumber();
                string cmsReleaseVersion = Settings.Default.SiteReleaseVersion;

                fullReleaseVersion = string.Format("{0}.{1}", cmsReleaseVersion, revisionVersion);

                //setting expiration based on svn file
                if (!string.IsNullOrEmpty(SvnFilePath))
                {
                    HttpContext.Current.Cache.Insert("CmsSvnVersion"
                                                        , fullReleaseVersion
                                                        , new CacheDependency(SvnFilePath)
                                                        , Cache.NoAbsoluteExpiration
                                                        , Cache.NoSlidingExpiration);
                }
                else
                {
                    HttpContext.Current.Cache.Insert("CmsSvnVersion"
                                                        , fullReleaseVersion
                                                        , null
                                                        , Cache.NoAbsoluteExpiration
                                                        , new TimeSpan(24, 0, 0));
                }
            }

            return HttpContext.Current.Cache["CmsSvnVersion"] as string;
        }

        private static string GetRevisionNumber()
        {
            if ((!File.Exists(SvnFilePath)))
            {
                return "0";
            }

            string revNumber = File.ReadAllText(SvnFilePath);
            return revNumber;
        }
    }

}