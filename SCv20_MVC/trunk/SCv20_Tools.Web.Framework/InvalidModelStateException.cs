using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCv20_Tools.Web.Framework {
    public class InvalidModelStateException : Exception {
        private readonly object _model;

        public InvalidModelStateException(object model) {
            this._model = model;
        }
    }
}
