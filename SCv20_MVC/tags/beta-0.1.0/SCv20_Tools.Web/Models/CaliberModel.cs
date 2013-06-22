using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Web.Models {
    public class CaliberModel {
        public CaliberModel() {
            var x = new Caliber();
        }

        public int CaliberId {
            get;
            set;
        }

        public string Value {
            get;
            set;
        }


        public static CaliberModel MapFrom(Caliber entity) {
            var model = new CaliberModel();

            if (entity == null)
                return model;

            model.CaliberId = entity.Id;
            model.Value = entity.Value;

            return model;
        }
    }
}