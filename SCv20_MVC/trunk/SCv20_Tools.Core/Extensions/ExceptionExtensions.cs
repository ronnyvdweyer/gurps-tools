using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCv20_Tools.Core.Extensions {
    public static class ExceptionExtensions {
        public static string GetInnerExceptionMessage(this Exception ex) {
            if (ex.InnerException != null) {
                return string.Format("{0} > {1} ", ex.InnerException.Message, GetInnerExceptionMessage(ex.InnerException));
            }
            return string.Empty;
        }
    }
}
