using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Custom.Framework.Core.Controller;
using Plus54PortfolioRedesign2014.Entities;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Drawing;

namespace Plus54PortfolioRedesign2014.BusinessLogic
{
    public class TechnologyBL : BaseController<Technology, Plus54PortfolioRedesign2014Entities>
    {
        #region Constructors

        public TechnologyBL(Plus54PortfolioRedesign2014Entities context) : base(context) { }

        #endregion

        #region Public Methods

        public void Create(string name, string thumbnailPath)
        {
            var technology = new Technology();
            technology.Name = name;
            technology.Thumbnail = new PortfolioImageBL(DBContext).Create(thumbnailPath, PortfolioImageBL.PortfolioImageType.Technology);

            CreateNew(technology);
        }

        public void Update(int id, string name, string thumbnailPath)
        {
            var technologyToUpdate = this.GetById(id);
            if (technologyToUpdate != null)
            {
                technologyToUpdate.Name = name;
                if (technologyToUpdate.Thumbnail != null && !technologyToUpdate.Thumbnail.Path.Equals(thumbnailPath))
                {
                    var portolioImageBL = new PortfolioImageBL(DBContext);
                    portolioImageBL.Delete(technologyToUpdate.Thumbnail.IdImage);
                    technologyToUpdate.Thumbnail = portolioImageBL.Create(thumbnailPath, PortfolioImageBL.PortfolioImageType.Technology);
                }
            }
        }

        public void Delete(int id)
        {
            var technologyToDelete = this.GetById(id);
            if (technologyToDelete != null)
            {
                new PortfolioImageBL(DBContext).Delete(technologyToDelete.Thumbnail.IdImage);
                technologyToDelete.Projects.Clear();
                Delete(technologyToDelete);
            }
        }

        public List<Technology> GetByFilter(string filter)
        {
            var technologies = new List<Technology>();
            technologies = DBContext.Technology.Where(t => t.Name.Contains(filter)).ToList();

            return technologies;
        }

        #endregion
    }
}
