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
    public class SocialMediaBL : BaseController<SocialMedia, Plus54PortfolioRedesign2014Entities>
    {
        #region Constructors

        public SocialMediaBL(Plus54PortfolioRedesign2014Entities context) : base(context) { }

        #endregion

        #region Public Methods

        public void Create(string name, string thumbnailPath)
        {
            var socialMedia = new SocialMedia();
            socialMedia.Name = name;
            socialMedia.Thumbnail = new PortfolioImageBL(DBContext).Create(thumbnailPath, PortfolioImageBL.PortfolioImageType.SocialMedia);

            CreateNew(socialMedia);
        }

        public void Update(int id, string name, string thumbnailPath)
        {
            var socialMediaToUpdate = this.GetById(id);
            if (socialMediaToUpdate != null)
            {
                socialMediaToUpdate.Name = name;
                if (socialMediaToUpdate.Thumbnail != null && !socialMediaToUpdate.Thumbnail.Path.Equals(thumbnailPath))
                {
                    var portolioImageBL = new PortfolioImageBL(DBContext);
                    portolioImageBL.Delete(socialMediaToUpdate.Thumbnail.IdImage);
                    socialMediaToUpdate.Thumbnail = portolioImageBL.Create(thumbnailPath, PortfolioImageBL.PortfolioImageType.SocialMedia);
                }
            }
        }

        public void Delete(int id)
        {
            var socialMediaToDelete = this.GetById(id);
            if (socialMediaToDelete != null)
            {
                new PortfolioImageBL(DBContext).Delete(socialMediaToDelete.Thumbnail.IdImage);
                socialMediaToDelete.Projects.Clear();
                Delete(socialMediaToDelete);
            }
        }

        public List<SocialMedia> GetByFilter(string filter)
        {
            var socialMedia = new List<SocialMedia>();
            socialMedia = DBContext.SocialMedia.Where(sm => sm.Name.Contains(filter)).ToList();

            return socialMedia;
        }

        #endregion
    }
}
