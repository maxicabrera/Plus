using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Plus54PortfolioRedesign2014.Entities;
using Custom.Framework.Core.Model.FileManagement;
using Plus54PortfolioRedesign2014.BusinessLogic;
using Custom.Framework.Core.Controller;

namespace Plus54PortfolioRedesign2014.BusinessLogic
{
    public class ModelController : BaseModelController<Plus54PortfolioRedesign2014Entities, Plus54PortfolioRedesign2014ConnectionProvider>
    {
        #region Private vars

        private ClientBL clientBL { get; set; }
        private PartnerBL partnerBL { get; set; }
        private PortfolioImageBL portfolioImageBL { get; set; }
        private ProjectBL projectBL { get; set; }
        private ProjectCategoryBL projectCategoryBL { get; set; }
        private ScreenSupportBL screenSupportBL { get; set; }
        private ProjectTypeTagBL projectTypeTagBL { get; set; }
        private SocialMediaBL socialMediaBL { get; set; }
        private TechnologyBL technologyBL { get; set; }
        private UserBL userBL { get; set; }

        #endregion

        #region Public props

        public ClientBL ClientBL
        {
            get
            {
                if (clientBL == null)
                    clientBL = new ClientBL(DBContext);
                return clientBL;
            }
        }

        public PartnerBL PartnerBL
        {
            get
            {
                if (partnerBL == null)
                    partnerBL = new PartnerBL(DBContext);
                return partnerBL;
            }
        }

        public PortfolioImageBL PortfolioImageBL
        {
            get
            {
                if (portfolioImageBL == null)
                    portfolioImageBL = new PortfolioImageBL(DBContext);
                return portfolioImageBL;
            }
        }

        public ProjectBL ProjectBL
        {
            get
            {
                if (projectBL == null)
                    projectBL = new ProjectBL(DBContext);
                return projectBL;
            }
        }

        public ProjectCategoryBL ProjectCategoryBL
        {
            get
            {
                if (projectCategoryBL == null)
                    projectCategoryBL = new ProjectCategoryBL(DBContext);
                return projectCategoryBL;
            }
        }

        public ScreenSupportBL ScreenSupportBL
        {
            get
            {
                if (screenSupportBL == null)
                    screenSupportBL = new ScreenSupportBL(DBContext);
                return screenSupportBL;
            }
        }

        public ProjectTypeTagBL ProjectTypeTagBL
        {
            get
            {
                if (projectTypeTagBL == null)
                    projectTypeTagBL = new ProjectTypeTagBL(DBContext);
                return projectTypeTagBL;
            }
        }

        public SocialMediaBL SocialMediaBL
        {
            get
            {
                if (socialMediaBL == null)
                    socialMediaBL = new SocialMediaBL(DBContext);
                return socialMediaBL;
            }
        }

        public TechnologyBL TechnologyBL
        {
            get
            {
                if (technologyBL == null)
                    technologyBL = new TechnologyBL(DBContext);
                return technologyBL;
            }
        }

        public UserBL UserBL
        {
            get
            {
                if (userBL == null)
                    userBL = new UserBL(DBContext);
                return userBL;
            }
        }
        
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        #endregion
    }
}
