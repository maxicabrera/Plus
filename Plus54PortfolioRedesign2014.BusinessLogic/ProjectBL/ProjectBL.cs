using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Custom.Framework.Core.Controller;
using Plus54PortfolioRedesign2014.Entities;
using System.Data.Objects.DataClasses;

namespace Plus54PortfolioRedesign2014.BusinessLogic
{
    public class ProjectBL : BaseController<Project, Plus54PortfolioRedesign2014Entities>
    {
        #region Constructors

        public ProjectBL(Plus54PortfolioRedesign2014Entities context) : base(context) { }

        #endregion

        #region Public Methods

        public void Create(string name, string overview, string url, string clientName, string partnerName, int year, string thumbnailPath, string siteLogoPath, List<string> sliderImages, List<int> projectType, List<int> idProjectCategory, List<int> technologies, List<int> socialMedia, List<int> screenSupport)
        {
            var project = new Project();
            project.Name = name;
            project.Overview = overview;
            project.Url = url;
            project.Client = new ClientBL(DBContext).GetByName(clientName);
            project.Partner = new PartnerBL(DBContext).GetByName(partnerName);
            project.Year = year;
            project.Thumbnail = new PortfolioImageBL(DBContext).Create(thumbnailPath, PortfolioImageBL.PortfolioImageType.ProjectThumbnail);
            project.SiteLogo = new PortfolioImageBL(DBContext).Create(siteLogoPath, PortfolioImageBL.PortfolioImageType.ProjectSiteLogo);
            project.CreatedDate = DateTime.Now;

            foreach (var sliderImagePath in sliderImages)
                if (!string.IsNullOrEmpty(sliderImagePath))
                    project.SliderImages.Add(new PortfolioImageBL(DBContext).Create(sliderImagePath, PortfolioImageBL.PortfolioImageType.ProjectSliderImage));

            var projectTypeTagBL = new ProjectTypeTagBL(DBContext);
            foreach (var projectTypeId in projectType)
            {
                project.ProjectTypeTags.Add(projectTypeTagBL.GetById(projectTypeId));
            }

            var projectCategoryBL = new ProjectCategoryBL(DBContext);
            foreach (var idCategory in idProjectCategory)
            {
                project.ProjectCategory.Add(projectCategoryBL.GetById(idCategory));
            }

            var technologyBL = new TechnologyBL(DBContext);
            foreach (var technologyId in technologies)
            {
                project.Technologies.Add(technologyBL.GetById(technologyId));
            }

            if (socialMedia != null)
            {
                var socialMediaBL = new SocialMediaBL(DBContext);
                foreach (var socialMediaId in socialMedia)
                {
                    project.SocialMedia.Add(socialMediaBL.GetById(socialMediaId));
                } 
            }

            var screenSupportBL = new ScreenSupportBL(DBContext);
            foreach (var screenSupportId in screenSupport)
            {
                project.ScreenSupport.Add(screenSupportBL.GetById(screenSupportId));
            }
            CreateNew(project);
        }

        public void Update(int id, string name, string overview, string url, string clientName, string partnerName, int year, string thumbnailPath, string siteLogoPath, List<string> sliderImages, List<string> deletedSliderImages, List<int> projectType, List<int> idProjectCategory, List<int> technologies, List<int> socialMedia, List<int> screenSupport)
        {
            var projectToUpdate = this.GetById(id);
            if (projectToUpdate != null)
            {
                projectToUpdate.Name = name;
                projectToUpdate.Overview = overview;
                projectToUpdate.Url = url;
                projectToUpdate.Year = year;

                if (!projectToUpdate.Client.Name.Equals(clientName))
                    projectToUpdate.Client = new ClientBL(DBContext).GetByName(clientName);

                if (!projectToUpdate.Partner.Name.Equals(partnerName))
                    projectToUpdate.Partner = new PartnerBL(DBContext).GetByName(partnerName);

                //update images
                var portfolioImageBL = new PortfolioImageBL(DBContext);

                if (siteLogoPath != null && !projectToUpdate.SiteLogo.Path.Equals(siteLogoPath))
                {
                    portfolioImageBL.Delete(projectToUpdate.SiteLogo.IdImage);
                    projectToUpdate.SiteLogo = portfolioImageBL.Create(siteLogoPath, PortfolioImageBL.PortfolioImageType.ProjectSiteLogo);
                }

                if (!projectToUpdate.Thumbnail.Path.Equals(thumbnailPath))
                {
                    portfolioImageBL.Delete(projectToUpdate.Thumbnail.IdImage);
                    projectToUpdate.Thumbnail = portfolioImageBL.Create(thumbnailPath, PortfolioImageBL.PortfolioImageType.ProjectThumbnail);
                }

                foreach (var sliderImagePath in sliderImages)
                    if(!string.IsNullOrEmpty(sliderImagePath))
                        projectToUpdate.SliderImages.Add(portfolioImageBL.Create(sliderImagePath, PortfolioImageBL.PortfolioImageType.ProjectSliderImage));

                foreach (var sliderImagePath in deletedSliderImages)
                {
                    if(!string.IsNullOrEmpty(sliderImagePath))
                    {
                        var sliderImage = portfolioImageBL.GetByPath(sliderImagePath);
                        if (sliderImage != null)
                        {
                            projectToUpdate.SliderImages.Remove(sliderImage);
                            portfolioImageBL.Delete(sliderImage.IdImage);
                        }
                    }
                }

                //update project type                
                var removedProjectTypes = projectToUpdate.ProjectTypeTags.Where(pt => !projectType.Contains(pt.IdProjectTypeTag)).ToList();
                foreach (var pt in removedProjectTypes)
                    projectToUpdate.ProjectTypeTags.Remove(pt);

                var projectTypeTagBL = new ProjectTypeTagBL(DBContext);
                foreach (var projectTypeId in projectType)
                    if(!projectToUpdate.ProjectTypeTags.Where(pt => pt.IdProjectTypeTag == projectTypeId).Any())
                        projectToUpdate.ProjectTypeTags.Add(projectTypeTagBL.GetById(projectTypeId));

                //update technologies                
                var removedTechnologies = projectToUpdate.Technologies.Where(t => !technologies.Contains(t.IdTechnology)).ToList();
                foreach (var t in removedTechnologies)
                    projectToUpdate.Technologies.Remove(t);

                var technologyBL = new TechnologyBL(DBContext);
                foreach (var technologyId in technologies)
                    if (!projectToUpdate.Technologies.Where(t => t.IdTechnology == technologyId).Any())
                        projectToUpdate.Technologies.Add(technologyBL.GetById(technologyId));

                //update social media                
                var removedSocialMedia = projectToUpdate.SocialMedia.Where(s => !socialMedia.Contains(s.IdSocialMedia)).ToList();
                foreach (var s in removedSocialMedia)
                    projectToUpdate.SocialMedia.Remove(s);

                if (socialMedia != null)
                {
                    var socialMediaBL = new SocialMediaBL(DBContext);
                    foreach (var socialMediaId in socialMedia)
                        if (!projectToUpdate.SocialMedia.Where(s => s.IdSocialMedia == socialMediaId).Any())
                            projectToUpdate.SocialMedia.Add(socialMediaBL.GetById(socialMediaId)); 
                }

                //update screen support              
                var removedScreenSupport = projectToUpdate.ScreenSupport.Where(s => !screenSupport.Contains(s.IdScreenSupport)).ToList();
                foreach (var s in removedScreenSupport)
                    projectToUpdate.ScreenSupport.Remove(s);

                var screenSupportBL = new ScreenSupportBL(DBContext);
                foreach (var screenSupportId in screenSupport)
                    if (!projectToUpdate.ScreenSupport.Where(s => s.IdScreenSupport == screenSupportId).Any())
                        projectToUpdate.ScreenSupport.Add(screenSupportBL.GetById(screenSupportId));

                //update category
                var removedProjectCategory = projectToUpdate.ProjectCategory.Where(s => !idProjectCategory.Contains(s.IdProjectCategory)).ToList();
                foreach (var pc in removedProjectCategory)
                    projectToUpdate.ProjectCategory.Remove(pc);

                var projectCategoryBL = new ProjectCategoryBL(DBContext);
                foreach (var ProjectCategoryId in idProjectCategory)
                    if (!projectToUpdate.ProjectCategory.Where(s => s.IdProjectCategory == ProjectCategoryId).Any())
                        projectToUpdate.ProjectCategory.Add(projectCategoryBL.GetById(ProjectCategoryId));
            }
        }

        public void Delete(int id)
        {
            var projectToDelete = this.GetById(id);
            if (projectToDelete != null)
            {
                var portfolioImageBL = new PortfolioImageBL(DBContext);

                var sliderImagesToDelete = projectToDelete.SliderImages.ToList();
                foreach (var sliderImage in sliderImagesToDelete)
                    portfolioImageBL.Delete(sliderImage.IdImage);

                //PORTFOLIO-82
                if (projectToDelete.SiteLogo != null)
                    portfolioImageBL.Delete(projectToDelete.SiteLogo.IdImage);

                portfolioImageBL.Delete(projectToDelete.Thumbnail.IdImage);

                projectToDelete.ProjectCategory.Clear();
                projectToDelete.Technologies.Clear();
                projectToDelete.SocialMedia.Clear();                
                projectToDelete.ProjectTypeTags.Clear();
                projectToDelete.ScreenSupport.Clear();

                Delete(projectToDelete);
            }
        }

        public List<Project> GetAllOrderedByCreatedDate()
        {
            return this.GetAll().OrderByDescending(p => p.CreatedDate).ToList();
        }

        public List<Project> GetAllByFilterAndData(string keywords, int[] technologies, int[] projectTypes)
        {
            IQueryable<Project> projectsQuery = this.GetAll();

            if(!string.IsNullOrEmpty(keywords))
                projectsQuery = projectsQuery.Where(p => p.Name.Contains(keywords) || p.Overview.Contains(keywords));
            
            if (technologies != null && technologies.Count() > 0)
            {
                projectsQuery = projectsQuery.Where(p => p.Technologies.Where(t => technologies.Contains(t.IdTechnology)).Any());
            }

            if (projectTypes != null && projectTypes.Count() > 0)
            {
                projectsQuery = projectsQuery.Where(p => p.ProjectTypeTags.Where(pt => projectTypes.Contains(pt.IdProjectTypeTag)).Any());
            }

            return projectsQuery.OrderByDescending(p => p.CreatedDate).ToList();
        }

        public List<Project> GetByNameAndData(string name, int client, int partner, int IdProjectCategory, int projectType, int technology, int socialMedia)
        {
            IQueryable<Project> projectsQuery = this.GetAll();

            if (!string.IsNullOrEmpty(name))
                projectsQuery = this.GetAll().Where(p => p.Name.Contains(name));

            if (client > 0)
                projectsQuery = projectsQuery.Where(p => p.Client != null && p.Client.IdClient == client);

            if (partner > 0)
                projectsQuery = projectsQuery.Where(p => p.Partner != null && p.Partner.IdPartner == partner);

            if (IdProjectCategory > 0)
                projectsQuery = projectsQuery.Where(p => p.ProjectCategory.Where(pc => pc.IdProjectCategory == IdProjectCategory).Any());

            if (projectType > 0)
                projectsQuery = projectsQuery.Where(p => p.ProjectTypeTags.Where(pt => pt.IdProjectTypeTag == projectType).Any());

            if (technology > 0)
                projectsQuery = projectsQuery.Where(p => p.Technologies.Where(t => t.IdTechnology == technology).Any());

            if (socialMedia > 0)
                projectsQuery = projectsQuery.Where(p => p.SocialMedia.Where(s => s.IdSocialMedia == socialMedia).Any());

            return projectsQuery.ToList();
        }


        #endregion
    }
}
