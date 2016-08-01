using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Admin.Models
{
    public class ProjectModel : BaseModel<ProjectModel, Plus54PortfolioRedesign2014.Entities.Project>
    {
        #region Public Properties

        public int IdProject { get; set; }

        [DisplayNameAttribute("Name")]
        [RequiredAttribute(ErrorMessage = "Name is required")]
        [CustomValidation(typeof(ProjectModel), "ValidateUniqueName")]
        public string Name { get; set; }

        [DisplayNameAttribute("Overview")]
        [RequiredAttribute(ErrorMessage = "Overview is required")]
        public string Overview { get; set; }

        [DisplayNameAttribute("Category")]
        [RequiredAttribute(ErrorMessage = "Category is required")]
        [CustomValidation(typeof(ProjectModel), "ValidateCategory")]
        public List<int> Category { get; set; }
        public List<string> CategoryName { get; set; }

        [DisplayNameAttribute("ScreenSupport")]
        [RequiredAttribute(ErrorMessage = "ScreenSupport is required")]
        public List<int> ScreenSupport { get; set; }
        public List<string> ScreenSupportNames { get; set; }

        [DisplayNameAttribute("Technologies")]
        [RequiredAttribute(ErrorMessage = "Technologies are required")]
        public List<int> Technologies { get; set; }
        public List<string> TechnologyNames { get; set; }

        [DisplayNameAttribute("Social Media")]
        public List<int> SocialMedia { get; set; }
        public List<string> SocialMediaNames { get; set; }

        [DisplayNameAttribute("Project Type(s)")]
        [RequiredAttribute(ErrorMessage = "Project Type(s) are required")]
        public List<int> ProjectType { get; set; }
        public List<string> ProjectTypeNames { get; set; }

        [DisplayNameAttribute("Url")]
        [RegularExpressionAttribute(@"^((https?|ftp)://[^\s/$.?#].[^\s]*)$", ErrorMessage = "Url is invalid")]
        public string Url { get; set; }

        [DisplayNameAttribute("Client")]
        [RequiredAttribute(ErrorMessage = "Client is required")]
        public string ClientName { get; set; }

        [DisplayNameAttribute("Partner")]
        [RequiredAttribute(ErrorMessage = "Partner is required")]
        public string PartnerName { get; set; }

        [DisplayNameAttribute("Year")]
        [RequiredAttribute(ErrorMessage = "Year is required")]
        [RegularExpressionAttribute(@"^19[5-9]\d|20[0-4]\d|2900$", ErrorMessage = "Year is invalid")]
        public int Year { get; set; }

        [DisplayNameAttribute("Thumbnail")]
        [RequiredAttribute(ErrorMessage = "Thumbnail image is required")]
        public string ThumbnailPath { get; set; }

        [DisplayNameAttribute("Site Logo")]
        public string SiteLogoPath { get; set; }

        [DisplayNameAttribute("Slider Images")]
        [RequiredAttribute(ErrorMessage = "Slider Images are required")]
        public List<string> SliderImages { get; set; }
        public List<string> DeletedSliderImages { get; set; }

        #endregion

        #region Constructors

        public ProjectModel() { }

        public ProjectModel(Entities.Project project)
        {
            this.IdProject = project.IdProject;
            this.Name = project.Name;
            this.Overview = project.Overview;
            this.Url = project.Url;

            if(project.Client != null)
            {
                this.ClientName = project.Client.Name;
            }

            if(project.Partner != null)
            {
                this.PartnerName = project.Partner.Name;
            }

            this.Year = project.Year;

            this.Technologies = new List<int>();
            this.TechnologyNames = new List<string>();
            if(project.Technologies.Any())
                foreach (var technology in project.Technologies)
                {
                    this.Technologies.Add(technology.IdTechnology);
                    this.TechnologyNames.Add(technology.Name);
                }

            this.ScreenSupport = new List<int>();
            this.ScreenSupportNames = new List<string>();
            if (project.ScreenSupport.Any())
                foreach (var screensupport in project.ScreenSupport)
                {
                    this.ScreenSupport.Add(screensupport.IdScreenSupport);
                    this.ScreenSupportNames.Add(screensupport.Name);
                }

            this.Category = new List<int>();
            this.CategoryName = new List<string>();
            if (project.ProjectCategory.Any())
            {
                this.Category = project.ProjectCategory.Select(pc => pc.IdProjectCategory).ToList();
                this.CategoryName = project.ProjectCategory.Select(pc => pc.Name).ToList();
            }                

            this.SocialMedia = new List<int>();
            this.SocialMediaNames = new List<string>();
            if (project.SocialMedia.Any())
                foreach (var socialMedia in project.SocialMedia)
                {
                    this.SocialMedia.Add(socialMedia.IdSocialMedia);
                    this.SocialMediaNames.Add(socialMedia.Name);
                }

            this.ProjectType = new List<int>();
            this.ProjectTypeNames = new List<string>();
            if (project.ProjectTypeTags.Any())
                foreach (var projectType in project.ProjectTypeTags)
                {
                    this.ProjectType.Add(projectType.IdProjectTypeTag);
                    this.ProjectTypeNames.Add(projectType.Name);                    
                }

            if (project.Thumbnail != null)
                this.ThumbnailPath = project.Thumbnail.Path;

            if (project.SiteLogo != null)
                this.SiteLogoPath = project.SiteLogo.Path;

            this.SliderImages = new List<string>();
            if(project.SliderImages.Any())
                foreach (var sliderImage in project.SliderImages)
                {
                    this.SliderImages.Add(sliderImage.Path);
                }
        }

        #endregion

        #region Validators Methods

        public static ValidationResult ValidateUniqueName(object value, ValidationContext context)
        {
            var reg = context.ObjectInstance as ProjectModel;

            if (new Plus54PortfolioRedesign2014.Entities.Plus54PortfolioRedesign2014Entities().Project.Any(p => p.Name.Equals(reg.Name) && p.IdProject != reg.IdProject))
                return new ValidationResult("Name must be unique");

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateCategory(object value, ValidationContext context)
        {
            //todo
            var reg = context.ObjectInstance as ProjectModel;

            if (!reg.Category.Any() /*|| ! new Plus54PortfolioRedesign2014.Entities.Plus54PortfolioRedesign2014Entities().ProjectCategory.Any(c => c.IdProjectCategory == reg.Category)*/)
                return new ValidationResult("Category is required");

            return ValidationResult.Success;
        }
        
        #endregion
    }
}