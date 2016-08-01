using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Plus54PortfolioRedesign2014.Web
{
    public class LocalizedStringLength : StringLengthAttribute
    {
        public LocalizedStringLength(int maximumLength)
            : base(maximumLength)
        {

        }

        public override string FormatErrorMessage(string name)
        {
            string message = string.Format(ResourcesHelper.GetMessageFromResource(ErrorMessageResourceName, ErrorMessageResourceType), MaximumLength.ToString());
            if (MinimumLength > 0)
            {
                message = string.Format(ResourcesHelper.GetMessageFromResource(ErrorMessageResourceName, ErrorMessageResourceType), MaximumLength.ToString(), MinimumLength.ToString());
            }
            return message;
        }

    }
}