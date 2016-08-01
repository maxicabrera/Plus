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
    public class ProjectTypeTagBL : BaseController<ProjectTypeTag, Plus54PortfolioRedesign2014Entities>
    {
        #region Constructors

        public ProjectTypeTagBL(Plus54PortfolioRedesign2014Entities context) : base(context) { }

        #endregion

        #region Public Methods

        public void Create(string name)
        {
            var projectTypeTag = new ProjectTypeTag();
            projectTypeTag.Name = name;

            CreateNew(projectTypeTag);
        }

        public void Update(int id, string name)
        {
            var projectTypeTagToUpdate = this.GetById(id);
            if (projectTypeTagToUpdate != null)
                projectTypeTagToUpdate.Name = name;
        }

        public void Delete(int id)
        {
            var projectTypeTagToDelete = this.GetById(id);
            if (projectTypeTagToDelete != null)
            {
                projectTypeTagToDelete.Projects.Clear();
                Delete(projectTypeTagToDelete);                    
            }
        }

        public List<ProjectTypeTag> GetByFilter(string filter)
        {
            var projectTypes = new List<ProjectTypeTag>();
            projectTypes = DBContext.ProjectTypeTag.Where(t => t.Name.Contains(filter)).ToList();

            return projectTypes;
        }

        #endregion
    }
}
