using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Models
{
    public class TechnologyModel : BaseModel<TechnologyModel, Plus54PortfolioRedesign2014.Entities.Technology>
    {
        public int IdTechnology { get; set; }
        public string Name { get; set; }

        public TechnologyModel() { }

        public TechnologyModel(Entities.Technology technology)
        {
            this.IdTechnology = technology.IdTechnology;
            this.Name = technology.Name;
        }
    }
}