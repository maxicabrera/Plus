using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace Plus54PortfolioRedesign2014.Web
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private PropertyInfo _nameProperty;
        private Type _resourceType;

        public LocalizedDisplayNameAttribute(string displayNameKey)
            : base(displayNameKey)
        {

        }

        public Type NameResourceType
        {
            get
            {
                return _resourceType;
            }
            set
            {
                _resourceType = value;
                //initialize nameProperty when type property is provided by setter
                _nameProperty = _resourceType.GetProperty(base.DisplayName, BindingFlags.Static | BindingFlags.Public);
            }
        }

        public override string DisplayName
        {
            get
            {
                //check if nameProperty is null and return original display name value
                if (_nameProperty == null)
                {
                    return base.DisplayName;
                }

                return GetMessageFromResource(base.DisplayName);
                //return (string)_nameProperty.GetValue(_nameProperty.DeclaringType, null);
            }
        }

        private string GetMessageFromResource(string resourceId)
        {
            return ResourcesHelper.GetMessageFromResource(resourceId, _resourceType);
        }
    }

}