using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Models
{
    public class ProjectTypeModel : BaseModel<ProjectTypeModel, Plus54PortfolioRedesign2014.Entities.ProjectTypeTag>
    {
        public int IdProjectTypeTag { get; set; }
        public string Name { get; set; }

        public ProjectTypeModel() { }

        public ProjectTypeModel(Entities.ProjectTypeTag projectTypeTag)
        {
            this.IdProjectTypeTag = projectTypeTag.IdProjectTypeTag;
            this.Name = projectTypeTag.Name;
        }
    }
}