using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Models
{
    public class ProjectModel : BaseModel<ProjectModel, Plus54PortfolioRedesign2014.Entities.Project>
    {

        public int id { get; set; }
        public string name { get; set; }
        public string partnerName { get; set; }
        public string types { get; set; }
        public string thumb { get; set; }
   

        public ProjectModel() { }

        public ProjectModel(Entities.Project project)
            : this()
        {
            this.id = project.IdProject;

            this.name = project.Name;

            this.partnerName = project.Partner.Name;

            if (project.Thumbnail != null)
                this.thumb = project.Thumbnail.Path;

            if (project.ProjectTypeTags.Any())
                this.types = string.Join(", ", project.ProjectTypeTags.Select(pt => pt.Name));
          
        }

    }
}