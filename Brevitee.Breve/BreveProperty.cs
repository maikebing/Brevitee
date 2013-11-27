using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Breve
{
    public class BreveProperty
    {
        public BreveProperty(string ownerClassName, string propertyName, string propertyType)
        {
            this.ClassName = ownerClassName;
            this.PropertyName = propertyName;
            this.PropertyType = propertyType;
        }

        public string PropertyType { get; set; }
        public string PropertyField { get; private set; }

        string _propertyName;
        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                _propertyName = value;
                PropertyField = "_{0}"._Format(_propertyName.CamelCase());
            }
        }

        public string ClassName { get; set; }
    }
}
