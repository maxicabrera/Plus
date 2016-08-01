using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using Plus54PortfolioRedesign2014.Web.Properties;
using Plus54PortfolioRedesign2014.BusinessLogic;
using System.Web.Hosting;

namespace Plus54PortfolioRedesign2014.Web.AppCode.Helpers
{
    public enum FolderPath
    {
        Technology = 1,
        ProjectCategory = 2,
        SocialMedia = 3,
        Project = 4,
        ScreenSupport = 5
    }

    public class FileHelper
    {
        public static string GetFileName(string filename, bool withExtension = true)
        {
            if ((string.IsNullOrEmpty(filename)))
            {
                return string.Empty;
            }

            if ((withExtension))
            {
                return Path.GetFileName(filename);
            }
            else
            {
                return Path.GetFileName(filename).Replace(Path.GetExtension(filename), string.Empty);
            }
        }

        public static string GetFileNameFromLink(string link, Guid id)
        {
            Match fileMatch = Regex.Match(link, "([^/]*)$");
            return fileMatch.Value.Replace(id.ToString() + "_", "");
        }

        public static string GetFileExtension(string filename)
        {
            return Path.GetExtension(filename);
        }

        public static string GetFileUrl(string fileField)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(fileField))
            {
                if (!fileField.StartsWith("/"))
                {
                    fileField = string.Format("/{0}", fileField);
                }
                result = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, fileField);
            }
            return result;
        }

        public static string GetFolderByFileType()//(EmailTypeEnum emailType)
        {
            return string.Empty;
        }

        protected static string SaveFile(string fileName, string folderRelativePath, HttpPostedFileBase file)
        {
            try
            {
                string fullname = string.Format("{0}{1}", Path.GetFileNameWithoutExtension(fileName), GetFileExtension(file.FileName));
                string relativeURL = string.Format("{0}/{1}", folderRelativePath, fullname);

                string localFullName = HostingEnvironment.MapPath(relativeURL);
                //save file to local folder
                if (!Directory.Exists(Path.GetDirectoryName(localFullName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localFullName));
                }

                file.SaveAs(localFullName);

                return relativeURL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveFile(string fileName, HttpPostedFileBase file)
        {
            try
            {
                string localFullName = fileName;
                //save file to local folder
                if (!Directory.Exists(Path.GetDirectoryName(localFullName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localFullName));
                }

                file.SaveAs(localFullName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string SaveFile(string fileName, FolderPath targetFolder, HttpPostedFileBase file)
        {
            try
            {
                //set file's full name into repository
                string folder = GetFolderPath(targetFolder);
                return SaveFile(fileName, folder, file);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.LogError("SaveFile", ex);
                throw;
            }
        }

        public static void DeleteFile(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    string rootedFilePath = filePath;
                    //set file's full name into repository
                    if ((!Path.IsPathRooted(rootedFilePath)))
                    {
                        if ((Path.GetPathRoot(rootedFilePath) != "\\\\"))
                        {
                            rootedFilePath = "/" + rootedFilePath;
                        }
                        rootedFilePath = HostingEnvironment.MapPath(rootedFilePath);
                    }

                    FileInfo fileInfo = new FileInfo(rootedFilePath);
                    //renaming file to _DELETED_[old file name]
                    if ((File.Exists(fileInfo.FullName)))
                    {
                        File.Delete(rootedFilePath);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";

            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileName(fileName);

                string ext = System.IO.Path.GetExtension(fileName).ToLower();
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                if ((regKey != null) && (regKey.GetValue("Content Type") != null))
                {
                    mimeType = regKey.GetValue("Content Type").ToString();
                }
            }
            return mimeType;
        }

        public static long GetFileLength(string filePath)
        {

            filePath = HostingEnvironment.MapPath(string.Format("~/{0}", filePath));

            if ((File.Exists(filePath)))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }

            return 0;
        }

        public static void CreateDirectoryIfNotExists(string directoryFolder)
        {
            if ((!Path.IsPathRooted(directoryFolder)))
            {
                directoryFolder = HostingEnvironment.MapPath("~") + Path.GetDirectoryName(directoryFolder);
            }

            if (!Directory.Exists(directoryFolder))
            {
                Directory.CreateDirectory(directoryFolder);
            }
        }

        public static string GetFullPath(string relativePath)
        {
            if ((!string.IsNullOrEmpty(relativePath)))
            {

                if ((!Path.IsPathRooted(relativePath)))
                {
                    if ((!relativePath.StartsWith("/")))
                    {
                        relativePath = "/" + relativePath;
                    }
                }

                return HostingEnvironment.MapPath("~" + relativePath);

            }
            return relativePath;
        }

        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public static string GetRelativeUrl(string file)
        {
            return file.StartsWith("/") ? file : string.Format("/{0}", file);
        }

        public static string CopyFile(string fileName, string pathFile)
        {
            try
            {
                string sourcePath = GetFullPath(pathFile);
                if (File.Exists(sourcePath))
                {
                    FileInfo file = new FileInfo(sourcePath);
                    //get file's name and extension
                    string fullname = string.Format("{0}{1}", fileName, file.Extension);
                    //set file's full name into repository
                    string folder = string.Empty;
                    string relativeURL = string.Format("{0}/{1}", folder, fullname);

                    string localFullName = HostingEnvironment.MapPath(relativeURL);

                    //save file to local folder
                    if (!Directory.Exists(Path.GetDirectoryName(localFullName)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(localFullName));
                    }

                    File.Copy(file.FullName, localFullName);

                    return relativeURL;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RenameFile(string source, string fileName)
        {
            if (File.Exists(source))
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                File.Move(source, fileName);
            }
        }

        public static string GetRelativeUrl(string file, FolderPath targetFolder)
        {
            return string.Format("{0}/{1}", GetFolderPath(targetFolder), file);
        }

        private static string GetFolderPath(FolderPath targetFolder)
        {
            string folder = string.Empty;
            switch (targetFolder)
            {
                case FolderPath.Technology:
                    folder = Settings.Default.TechnologyFolder;
                    break;
                case FolderPath.ProjectCategory:
                    folder = Settings.Default.ProjectCategoryFolder;
                    break;
                case FolderPath.SocialMedia:
                    folder = Settings.Default.SocialMediaFolder;
                    break;
                case FolderPath.Project:
                    folder = Settings.Default.ProjectFolder;
                    break;
                case FolderPath.ScreenSupport:
                    folder = Settings.Default.ScreenSupport;
                    break;
                default:
                    folder = Settings.Default.TempFolder;
                    break;
            }
            return folder;
        }
    }
}