using Plus54PortfolioRedesign2014.Web.AppCode.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.Admin.Models
{
    public class ClientModel : BaseModel<ClientModel, Plus54PortfolioRedesign2014.Entities.Client>
    {
        #region Public Properties

        public int IdClient { get; set; }  

        public string Name { get; set; }

        #endregion

        #region Constructors

        public ClientModel() { }

        public ClientModel(Entities.Client client)
        {
            this.IdClient = client.IdClient;
            this.Name = client.Name;
        }

        #endregion
    }
}