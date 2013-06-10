using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCv20_Tools.Core.Data {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreOnUpdateAttribute : Attribute {
    }
}
