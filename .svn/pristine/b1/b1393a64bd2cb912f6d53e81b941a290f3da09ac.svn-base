using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Admin.Models
{
    public class TechnologyModel : BaseModel<TechnologyModel, Plus54PortfolioRedesign2014.Entities.Technology>
    {
        #region Public Properties

        public int IdTechnology { get; set; }

        [DisplayNameAttribute("Name")]
        [RequiredAttribute(ErrorMessage = "Name is required")]
        [CustomValidation(typeof(TechnologyModel), "ValidateUniqueName")]
        public string Name { get; set; }

        [DisplayNameAttribute("Thumbnail")]
        [RequiredAttribute(ErrorMessage = "Thumbnail image is required")]
        public string ThumbnailPath { get; set; }

        public bool ThumbnailImageExists { get; set; }
        public string ThumbnailImageUrl { get; set; }

        #endregion

        #region Constructors

        public TechnologyModel() { }

        public TechnologyModel(Entities.Technology technology)
        {
            this.IdTechnology = technology.IdTechnology;
            this.Name = technology.Name;

            if (technology.Thumbnail != null)
                this.ThumbnailPath = technology.Thumbnail.Path;
        }

        public TechnologyModel(Entities.Technology technology, string urlPath = "")
            : this(technology)
        {
            if (technology.Thumbnail != null)
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
            var reg = context.ObjectInstance as TechnologyModel;

            if (new Plus54PortfolioRedesign2014.Entities.Plus54PortfolioRedesign2014Entities().Technology.Any(t => t.Name.Equals(reg.Name) && t.IdTechnology != reg.IdTechnology))
                return new ValidationResult("Name must be unique");

            return ValidationResult.Success;
        }

        #endregion
    }
}