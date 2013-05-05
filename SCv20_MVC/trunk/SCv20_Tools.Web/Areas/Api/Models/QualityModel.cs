using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCv20_Tools.Core;

namespace SCv20_Tools.Web.Areas.Api.Models {
    public class QualityModel {

        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public bool isSeasonsOnly { get; set; }

        public int bonusXP { get; set; }

        public int bonusAD { get; set; }

        public string isSeasonsOnlyFormatted {
            get {
                return (isSeasonsOnly ? "Seasons Only" : "Universal");
            }
        }

        public string bonusADFormated {
            get {
                return bonusAD.ToString("+#;-#;+0") + " AD";
            }
        }

        public string bonusXPFormated {
            get {
                return bonusXP.ToString("+#;-#;+0") + " XP";
            }
        }

        public static QualityModel MapFrom(Quality entity) {
            var model = new QualityModel();

            if (entity != null) {
                model.id = entity.Id;
                model.name = entity.Name;
                model.description = entity.Description;
                model.isSeasonsOnly = entity.IsSeasonsOnly;
                model.bonusXP = entity.BonusXP;
                model.bonusAD = entity.BonusAD;

            }
            return model;
        }
    }
}