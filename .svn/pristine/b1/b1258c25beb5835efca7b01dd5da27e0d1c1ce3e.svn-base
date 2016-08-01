using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Admin.Models
{
    public class ScreenSupportModel : BaseModel<ScreenSupportModel, Plus54PortfolioRedesign2014.Entities.ScreenSupport>
    {
        #region Public Properties

        public int IdScreenSupport { get; set; }

        [DisplayNameAttribute("Name")]
        [RequiredAttribute(ErrorMessage = "Name is required")]
        [CustomValidation(typeof(ScreenSupportModel), "ValidateUniqueName")]
        public string Name { get; set; }

        [DisplayNameAttribute("Thumbnail")]
        [RequiredAttribute(ErrorMessage = "Thumbnail image is required")]
        public string ThumbnailPath { get; set; }

        public bool ThumbnailImageExists { get; set; }
        public string ThumbnailImageUrl { get; set; }

        #endregion

        #region Constructors

        public ScreenSupportModel() { }

        public ScreenSupportModel(Entities.ScreenSupport screenSupport)
        {
            this.IdScreenSupport = screenSupport.IdScreenSupport;
            this.Name = screenSupport.Name;

            if (screenSupport.Thumbnail != null)
                this.ThumbnailPath = screenSupport.Thumbnail.Path;
        }

        public ScreenSupportModel(Entities.ScreenSupport screenSupport, string urlPath = "")
            : this(screenSupport)
        {
            if (screenSupport.Thumbnail != null)
            {
                this.ThumbnailImageUrl = urlPath;
                var filePath = FileHelper.GetFullPath(this.ThumbnailPath);
                this.ThumbnailImageExists = System.IO.File.Exists(filePath);
            }
        }

        #endregion

        #region Validators Methods

        public static ValidationResult ValidateUniqueName(object value, ValidationContext context)
        {
            var reg = context.ObjectInstance as ScreenSupportModel;

            if (new Plus54PortfolioRedesign2014.Entities.Plus54PortfolioRedesign2014Entities().ScreenSupport.Any(ss => ss.Name.Equals(reg.Name) && ss.IdScreenSupport != reg.IdScreenSupport))
                return new ValidationResult("Name must be unique");

            return ValidationResult.Success;
        }

        #endregion
    }
}