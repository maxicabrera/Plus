using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace Plus54PortfolioRedesign2014.Web
{
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return ResourcesHelper.GetMessageFromResource(ErrorMessageResourceName, ErrorMessageResourceType);
        }
    }
}
