using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Admin.Models
{
    public class PartnerModel : BaseModel<PartnerModel, Plus54PortfolioRedesign2014.Entities.Partner>
    {
        #region Public Properties

        public int IdPartner { get; set; }

        public string Name { get; set; }

        #endregion

        #region Constructors

        public PartnerModel() { }

        public PartnerModel(Entities.Partner partner)
        {
            this.IdPartner = partner.IdPartner;
            this.Name = partner.Name;
        }

        #endregion
    }
}