using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Admin.Models
{
    public class ProjectCategoryModel : BaseModel<ProjectCategoryModel, Plus54PortfolioRedesign2014.Entities.ProjectCategory>
    {
        #region Public Properties

        public int IdProjectCategory { get; set; }

        [DisplayNameAttribute("Name")]
        [RequiredAttribute(ErrorMessage = "Name is required")]
        [CustomValidation(typeof(ProjectCategoryModel), "ValidateUniqueName")]
        public string Name { get; set; }

        [DisplayNameAttribute("Thumbnail")]
        [RequiredAttribute(ErrorMessage = "Thumbnail image is required")]
        public string ThumbnailPath { get; set; }

        public bool ThumbnailImageExists { get; set; }
        public string ThumbnailImageUrl { get; set; }

        #endregion

        #region Constructors

        public ProjectCategoryModel() { }

        public ProjectCategoryModel(Entities.ProjectCategory projectCategory)
        {
            this.IdProjectCategory = projectCategory.IdProjectCategory;
            this.Name = projectCategory.Name;

            if (projectCategory.Thumbnail != null)
                this.ThumbnailPath = projectCategory.Thumbnail.Path;
        }

        public ProjectCategoryModel(Entities.ProjectCategory projectCategory, string urlPath = "")
            : this(projectCategory)
        {
            if (projectCategory.Thumbnail != null)
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
            var reg = context.ObjectInstance as ProjectCategoryModel;

            if (new Plus54PortfolioRedesign2014.Entities.Plus54PortfolioRedesign2014Entities().ProjectCategory.Any(pc => pc.Name.Equals(reg.Name) && pc.IdProjectCategory != reg.IdProjectCategory))
                return new ValidationResult("Name must be unique");

            return ValidationResult.Success;
        }

        #endregion
    }
}