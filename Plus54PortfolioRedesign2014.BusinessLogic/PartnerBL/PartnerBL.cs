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
    public class PartnerBL : BaseController<Partner, Plus54PortfolioRedesign2014Entities>
    {
        #region Constructors

        public PartnerBL(Plus54PortfolioRedesign2014Entities context) : base(context) { }

        #endregion

        #region Public Methods

        public Partner GetByName(string name)
        {
            var partner = this.GetByNameInternal(name);

            if (partner == null)
                partner = this.Create(name);

            return partner;
        }

        private Partner Create(string name)
        {
            var partner = new Partner();
            partner.Name = name;

            CreateNew(partner);
            return partner;
        }

        private Partner GetByNameInternal(string name)
        {
            return DBContext.Partner.Where(c => c.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
        }

        #endregion
    }
}
