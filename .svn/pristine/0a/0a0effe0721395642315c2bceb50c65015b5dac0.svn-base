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
    public class UserBL
    {
        #region Constructors

        public UserBL(Plus54PortfolioRedesign2014Entities context) { }

        public bool ValidatePlayerLogOn(string username, string password)
        {
            if (username.Equals(Settings.Default.UserName) && password.Equals(Settings.Default.Password))
                return true;
            else
                return false;
        }

        #endregion
    }
}
