using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCv20_Tools.Core;

namespace SCv20_Tools.Web.Models {
    public class QualityModel {
        public static readonly object x = new object();

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsSeasonsOnly { get; set; }

        public int BonusXP { get; set; }

        public int BonusAD { get; set; }

        public string IsSeasonsOnlyFormatted {
            get {
                return (IsSeasonsOnly ? "Seasons Only" : "Universal");
            }
        }

        public string BonusADFormated {
            get {
                return BonusAD.ToString("+#;-#;+0") + " AD";
            }
        }

        public string BonusXPFormated {
            get {
                return BonusXP.ToString("+#;-#;+0") + " XP";
            }
        }

        public static QualityModel MapFrom(Quality entity) {
            lock (x) {
                var model = new QualityModel();

                if (entity != null) {
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.Description = entity.Description;
                    model.IsSeasonsOnly = entity.IsSeasonsOnly;
                    model.BonusXP = entity.BonusXP;
                    model.BonusAD = entity.BonusAD;

                }
                return model;
            }
        }
    }
}