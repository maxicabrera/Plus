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
    public class ClientBL : BaseController<Client, Plus54PortfolioRedesign2014Entities>
    {
        #region Constructors

        public ClientBL(Plus54PortfolioRedesign2014Entities context) : base(context) { }

        #endregion

        #region Public Methods

        public Client GetByName(string name)
        {
            var client = this.GetByNameInternal(name);

            if (client == null)
                client = this.Create(name);

            return client;
        }

        private Client Create(string name)
        {
            var client = new Client();
            client.Name = name;

            CreateNew(client);
            return client;
        }

        private Client GetByNameInternal(string name)
        {
            return DBContext.Client.Where(c => c.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
        }

        #endregion
    }
}
