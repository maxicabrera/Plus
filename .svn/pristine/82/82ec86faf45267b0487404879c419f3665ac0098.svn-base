using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Custom.Framework.Core.Controller;
using Plus54PortfolioRedesign2014.Entities;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace Plus54PortfolioRedesign2014.BusinessLogic
{
    public class PortfolioImageBL : BaseController<PortfolioImage, Plus54PortfolioRedesign2014Entities>
    {
        public enum PortfolioImageType
        {
            [Description("Technology")]
            Technology = 1,
            [Description("ProjectCategory")]
            ProjectCategory = 2,
            [Description("SocialMedia")]
            SocialMedia = 3,
            [Description("ProjectThumbnail")]
            ProjectThumbnail = 4,
            [Description("ProjectSiteLogo")]
            ProjectSiteLogo = 5,
            [Description("ProjectSliderImage")]
            ProjectSliderImage = 6,
            [Description("ScreenSupportImage")]
            ScreenSupportImage = 7
        }

        #region Constructors

        public PortfolioImageBL(Plus54PortfolioRedesign2014Entities context) : base(context) { }

        #endregion

        #region Public Methods

        internal PortfolioImage Create(string imageRelativePath, PortfolioImageType portfolioImageType)
        {
            //creat new portfolio image
            var portfolioImage = new PortfolioImage();

            //get uploaded image full path
            var imageFullPath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, imageRelativePath);

            if (File.Exists(imageFullPath))
            {
         
                //manually call to GC to free the image files opened
                GC.Collect();

                //open the image
                using (var image = Image.FromFile(imageFullPath))
                {
                    portfolioImage.Path = imageRelativePath;
                    portfolioImage.Height = image.Height;
                    portfolioImage.Width = image.Width;
                    portfolioImage.Type = GetImageType(image);
                }

                CreateNew(portfolioImage);

                return portfolioImage;
            }

            return null;
        }

        internal void Delete(int id)
        {
            try
            {
                var portfolioImageToDelete = this.GetById(id);

                if (portfolioImageToDelete != null)
                {
                    var imageFullPath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, portfolioImageToDelete.Path);
                    if (File.Exists(imageFullPath))
                        File.Delete(imageFullPath);

                    Delete(portfolioImageToDelete);
                }
            }
            catch (Exception e) { }
        }

        internal PortfolioImage GetByPath(string path)
        {
            return DBContext.PortfolioImage.Where(pi => pi.Path.ToLower().Equals(path.ToLower())).FirstOrDefault();
        }

        #endregion

        #region Private Methods

        private String GetImageType(Image img)
        {
            var imageCodec = ImageCodecInfo.GetImageDecoders().Where(id => id.FormatID.Equals(img.RawFormat.Guid)).FirstOrDefault();

            if (imageCodec != null)
                return imageCodec.MimeType;
            else
                return "image/unknown";
        }

        private String CheckSize(Image image, string imageFullPath, PortfolioImageType portfolioImageType)
        {
            //Settings.Default.Width < 0: has no limits
            //Settings.Default.Height < 0: has no limits

            //load settings values
            //var predefinedWidth = -1;
            //var predefinedHeight = -1;
            //var predefinedMaxHeight = -1;
            //var predefinedMaxWidth = -1;

            //switch (portfolioImageType)
            //{
            //    case PortfolioImageType.Technology:
            //        predefinedWidth = Settings.Default.TechnologyThumbnailWidth;
            //        predefinedHeight = Settings.Default.TechnologyThumbnailHeight;
            //        break;
            //    case PortfolioImageType.ProjectCategory:
            //        predefinedWidth = Settings.Default.ProjectCategoryThumbnailWidth;
            //        predefinedHeight = Settings.Default.ProjectCategoryThumbnailHeight;
            //        predefinedMaxHeight = Settings.Default.ProjectCategoryThumbnailMaxHeight;
            //        break;
            //    case PortfolioImageType.SocialMedia:
            //        predefinedWidth = Settings.Default.SocialMediaThumbnailWidth;
            //        predefinedHeight = Settings.Default.SocialMediaThumbnailHeight;
            //        break;
            //    case PortfolioImageType.ProjectThumbnail:
            //        predefinedWidth = Settings.Default.ProjectThumbnailWidth;
            //        predefinedHeight = Settings.Default.ProjectThumbnailHeight;
            //        break;
            //    case PortfolioImageType.ProjectSiteLogo:
            //        predefinedWidth = Settings.Default.ProjectSiteLogoWidth;
            //        predefinedHeight = Settings.Default.ProjectSiteLogoHeight;
            //        break;
            //    case PortfolioImageType.ProjectSliderImage:
            //        predefinedWidth = Settings.Default.ProjectSliderImageWidth;
            //        predefinedHeight = Settings.Default.ProjectSliderImageHeight;
            //        break;
            //}

            //create new image path replacing the original GUID with a new one
            var newImagePath = imageFullPath.Replace(Path.GetFileNameWithoutExtension(imageFullPath), Guid.NewGuid().ToString());

            ////check if is needed to change the height and width of the image
            //if (predefinedWidth > 0 && predefinedHeight > 0 && image.Width != predefinedWidth && image.Height != predefinedHeight)
            //{
            //    Custom.Framework.Utilities.ImageResizer.ResizeImageByImagePath(imageFullPath, newImagePath, predefinedWidth, predefinedHeight, false);
            //    return newImagePath;
            //}

            ////check if is needed to change only the width of the image
            //if (predefinedWidth > 0 && image.Width != predefinedWidth)
            //{
            //    Custom.Framework.Utilities.ImageResizer.ResizeImageByImagePath(imageFullPath, newImagePath, predefinedWidth, image.Height, false);
            //    return newImagePath;
            //}

            //if (predefinedMaxWidth > 0 && image.Width > predefinedMaxWidth)
            //{
            //    Custom.Framework.Utilities.ImageResizer.ResizeImageByImagePath(imageFullPath, newImagePath, predefinedMaxWidth, image.Height, false);
            //    return newImagePath;
            //}

            ////check if is needed to change only the height of the image
            //if (predefinedHeight > 0 && image.Height != predefinedHeight)
            //{
            //    Custom.Framework.Utilities.ImageResizer.ResizeImageByImagePath(imageFullPath, newImagePath, image.Width, predefinedHeight, false);
            //    return newImagePath;
            //}

            //if (predefinedMaxHeight > 0 && image.Height > predefinedMaxHeight)
            //{
            //    Custom.Framework.Utilities.ImageResizer.ResizeImageByImagePath(imageFullPath, newImagePath, image.Width, predefinedMaxHeight, false);
            //    return newImagePath;
            //}

            return imageFullPath;
        }

        #endregion
    }
}
