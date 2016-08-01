using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Admin.Models
{
    public class ProjectTypeModel : BaseModel<ProjectTypeModel, Plus54PortfolioRedesign2014.Entities.ProjectTypeTag>
    {
        #region Public Properties

        public int IdProjectTypeTag { get; set; }

        [DisplayNameAttribute("Name")]
        [RequiredAttribute(ErrorMessage = "Name is required")]
        [CustomValidation(typeof(ProjectTypeModel), "ValidateUniqueName")]
        public string Name { get; set; }

        #endregion

        #region Constructors

        public ProjectTypeModel() { }

        public ProjectTypeModel(Entities.ProjectTypeTag projectTypeTag)
        {
            this.IdProjectTypeTag = projectTypeTag.IdProjectTypeTag;
            this.Name = projectTypeTag.Name;
        }

        #endregion

        #region Validators Methods

        public static ValidationResult ValidateUniqueName(object value, ValidationContext context)
        {
            var reg = context.ObjectInstance as ProjectTypeModel;

            if (new Plus54PortfolioRedesign2014.Entities.Plus54PortfolioRedesign2014Entities().ProjectTypeTag.Any(t => t.Name.Equals(reg.Name) && t.IdProjectTypeTag != reg.IdProjectTypeTag))
                return new ValidationResult("Name must be unique");

            return ValidationResult.Success;
        }

        #endregion
    }
}