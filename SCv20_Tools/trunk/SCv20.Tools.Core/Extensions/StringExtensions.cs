using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System {
    public static class StringExtensions {
        public static string FormatWith(this string format, params object[] args) {
            if (format == null)
                throw new ArgumentNullException("format");
            return string.Format(format, args);
        }

        
        public static string FormatWith(this string format, IFormatProvider provider, params object[] args) {
            if (format == null)
                throw new ArgumentNullException("format");
            return string.Format(provider, format, args);
        }


        public static Int32 SafeInt32(this string value) { 
            int ret = 0;
            var res = Int32.TryParse(value, out ret);
            return ret;
        }
    }
}
