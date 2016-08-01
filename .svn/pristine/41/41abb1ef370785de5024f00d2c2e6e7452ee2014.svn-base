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
    public class ProjectCategoryBL : BaseController<ProjectCategory, Plus54PortfolioRedesign2014Entities>
    {
        #region Constructors

        public ProjectCategoryBL(Plus54PortfolioRedesign2014Entities context) : base(context) { }

        #endregion

        #region Public Methods

        public void Create(string name, string thumbnailPath)
        {
            var projectCategory = new ProjectCategory();
            projectCategory.Name = name;
            projectCategory.Thumbnail = new PortfolioImageBL(DBContext).Create(thumbnailPath, PortfolioImageBL.PortfolioImageType.ProjectCategory);

            CreateNew(projectCategory);
        }

        public void Update(int id, string name, string thumbnailPath)
        {
            var projectCategoryToUpdate = this.GetById(id);
            if (projectCategoryToUpdate != null)
            {
                projectCategoryToUpdate.Name = name;
                if (projectCategoryToUpdate.Thumbnail != null && !projectCategoryToUpdate.Thumbnail.Path.Equals(thumbnailPath))
                {
                    var portolioImageBL = new PortfolioImageBL(DBContext);
                    portolioImageBL.Delete(projectCategoryToUpdate.Thumbnail.IdImage);
                    projectCategoryToUpdate.Thumbnail = portolioImageBL.Create(thumbnailPath, PortfolioImageBL.PortfolioImageType.ProjectCategory);
                }
            }
        }

        public void Delete(int id)
        {
            var projectCategoryToDelete = this.GetById(id);
            if (projectCategoryToDelete != null)
            {
                new PortfolioImageBL(DBContext).Delete(projectCategoryToDelete.Thumbnail.IdImage);
                projectCategoryToDelete.Projects.Clear();
                Delete(projectCategoryToDelete);
            }
        }

        public List<ProjectCategory> GetByFilter(string filter)
        {
            var projectCategories = new List<ProjectCategory>();
            projectCategories = DBContext.ProjectCategory.Where(pc => pc.Name.Contains(filter)).ToList();

            return projectCategories;
        }

        #endregion
    }
}
