using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Admin.Models
{
    public class SocialMediaModel : BaseModel<SocialMediaModel, Plus54PortfolioRedesign2014.Entities.SocialMedia>
    {
        #region Public Properties

        public int IdSocialMedia { get; set; }

        [DisplayNameAttribute("Name")]
        [RequiredAttribute(ErrorMessage = "Name is required")]
        [CustomValidation(typeof(SocialMediaModel), "ValidateUniqueName")]
        public string Name { get; set; }

        [DisplayNameAttribute("Thumbnail")]
        [RequiredAttribute(ErrorMessage = "Thumbnail image is required")]
        public string ThumbnailPath { get; set; }

        public bool ThumbnailImageExists { get; set; }
        public string ThumbnailImageUrl { get; set; }

        #endregion

        #region Constructors

        public SocialMediaModel() { }

        public SocialMediaModel(Entities.SocialMedia socialMedia)
        {
            this.IdSocialMedia = socialMedia.IdSocialMedia;
            this.Name = socialMedia.Name;

            if (socialMedia.Thumbnail != null)
                this.ThumbnailPath = socialMedia.Thumbnail.Path;
        }

        public SocialMediaModel(Entities.SocialMedia socialMedia, string urlPath = "")
            : this(socialMedia)
        {
            if (socialMedia.Thumbnail != null)
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
            var reg = context.ObjectInstance as SocialMediaModel;

            if (new Plus54PortfolioRedesign2014.Entities.Plus54PortfolioRedesign2014Entities().SocialMedia.Any(sm => sm.Name.Equals(reg.Name) && sm.IdSocialMedia != reg.IdSocialMedia))
                return new ValidationResult("Name must be unique");

            return ValidationResult.Success;
        }

        #endregion
    }
}