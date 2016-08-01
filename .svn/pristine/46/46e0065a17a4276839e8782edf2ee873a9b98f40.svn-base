using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using Plus54PortfolioRedesign2014.BusinessLogic;

namespace Plus54PortfolioRedesign2014.Web
{
    public class BasePage : System.Web.UI.Page
    {
        protected ModelController _ModelController;
        //<summary>
        // Provides access to the ModelController for every UserControl.
        //</summary>
        public ModelController ModelController
        {
            get
            {
                if (_ModelController == null)
                {
                    _ModelController = new ModelController();
                }

                return _ModelController;
            }
            set { _ModelController = value; }
        }
    }
}