using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Models
{
    public class ProjectDetailModel : BaseModel<ProjectDetailModel, Plus54PortfolioRedesign2014.Entities.Project>
    {
        public int id { get; set; }
        public string name { get; set; }
        public string partner { get; set; }
        public string client { get; set; }
        public string year { get; set; }
        public string link { get; set; }
        public string description { get; set; }

        public string client_logo { get; set; }

        public List<string> project_types { get; set; }

        public List<string> category_img { get; set; }
        public List<string> social_img { get; set; }
        public List<string> technologies_img { get; set; }
        public List<string> screensupport_img { get; set; }
        public List<string> image_large { get; set; }

        public ProjectDetailModel()
        {
            screensupport_img = new List<string>();
            technologies_img = new List<string>();
            social_img = new List<string>();
            image_large = new List<string>();
            project_types = new List<string>();
            category_img = new List<string>();
        }

        public ProjectDetailModel(Entities.Project project)
            : this()
        {
            if (project != null)
            {
                this.id = project.IdProject;
                this.name = project.Name;
                this.year = project.Year.ToString();
                this.link = project.Url;
                this.description = project.Overview;

                if (project.Partner != null)
                    this.partner = project.Partner.Name;

                if (project.Client != null)
                    this.client = project.Client.Name;

                if (project.SiteLogo != null)
                    this.client_logo = project.SiteLogo.Path;

                if (project.ProjectCategory != null && project.ProjectCategory.Any())
                    foreach (var pc in project.ProjectCategory)
                    {
                        if (pc.Thumbnail != null)
                            category_img.Add(pc.Thumbnail.Path);
                    }

                if (project.ScreenSupport != null && project.ScreenSupport.Any())
                    foreach (var ss in project.ScreenSupport)
                    {
                        if (ss.Thumbnail != null)
                            screensupport_img.Add(ss.Thumbnail.Path);
                    }

                if (project.Technologies != null && project.Technologies.Any())
                    foreach (var t in project.Technologies)
                    {
                        if (t.Thumbnail != null)
                            technologies_img.Add(t.Thumbnail.Path);
                    }

                if (project.SocialMedia != null && project.SocialMedia.Any())
                    foreach (var s in project.SocialMedia)
                    {
                        if (s.Thumbnail != null)
                            social_img.Add(s.Thumbnail.Path);
                    }

                if (project.ProjectTypeTags != null && project.ProjectTypeTags.Any())
                    foreach (var pt in project.ProjectTypeTags)
                        project_types.Add(pt.Name);

                if (project.SliderImages != null && project.SliderImages.Any())
                    foreach (var s in project.SliderImages)
                        image_large.Add(s.Path);
            }
        }
    }
}