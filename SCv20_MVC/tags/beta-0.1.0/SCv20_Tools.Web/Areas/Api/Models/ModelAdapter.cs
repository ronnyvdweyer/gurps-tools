using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCv20_Tools.Web.Areas.Api.Models {
    public class ModelAdapter<T> {
        public bool isValid { get; set; }
        public T d { get; set; }
    }
}