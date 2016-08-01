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
    public class ScreenSupportBL : BaseController<ScreenSupport, Plus54PortfolioRedesign2014Entities>
    {
        #region Constructors

        public ScreenSupportBL(Plus54PortfolioRedesign2014Entities context) : base(context) { }

        #endregion

        #region Public Methods

        public void Create(string name, string thumbnailPath)
        {
            var screenSupport = new ScreenSupport();
            screenSupport.Name = name;
            screenSupport.Thumbnail = new PortfolioImageBL(DBContext).Create(thumbnailPath, PortfolioImageBL.PortfolioImageType.ScreenSupportImage);

            CreateNew(screenSupport);
        }

        public void Update(int id, string name, string thumbnailPath)
        {
            var screenSupportToUpdate = this.GetById(id);
            if (screenSupportToUpdate != null)
            {
                screenSupportToUpdate.Name = name;
                if (screenSupportToUpdate.Thumbnail != null && !screenSupportToUpdate.Thumbnail.Path.Equals(thumbnailPath))
                {
                    var portolioImageBL = new PortfolioImageBL(DBContext);
                    portolioImageBL.Delete(screenSupportToUpdate.Thumbnail.IdImage);
                    screenSupportToUpdate.Thumbnail = portolioImageBL.Create(thumbnailPath, PortfolioImageBL.PortfolioImageType.ScreenSupportImage);
                }
            }
        }

        public void Delete(int id)
        {
            var screenSupportToDelete = this.GetById(id);
            if (screenSupportToDelete != null)
            {
                new PortfolioImageBL(DBContext).Delete(screenSupportToDelete.Thumbnail.IdImage);
                screenSupportToDelete.Projects.Clear();
                Delete(screenSupportToDelete);
            }
        }

        public List<ScreenSupport> GetByFilter(string filter)
        {
            var screenSupport = new List<ScreenSupport>();
            screenSupport = DBContext.ScreenSupport.Where(pc => pc.Name.Contains(filter)).ToList();

            return screenSupport;
        }

        #endregion
    }
}
