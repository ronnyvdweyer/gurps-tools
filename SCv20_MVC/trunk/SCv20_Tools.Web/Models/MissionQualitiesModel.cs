using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCv20_Tools.Web.Models {
    public class MissionQualitiesModel {
        public MissionQualitiesModel() {
            Qualities = new List<QualityModel>();
        }

        /// <summary>
        /// Gets or Sets the mission ID.
        /// </summary>
        public int? ID {
            get;
            set;
        }

        public int SelectedQualityId {
            get;
            set;
        }

        public List<QualityModel> Qualities {
            get;
            set;
        }

        public List<QualityModel> AvaliableQualities {
            get;
            set;
        }


        public int BonusXPCalculated {
            get {
                return Qualities.Sum(item => item.BonusXP);
            }
        }

        public int BonusADCalculated {
            get {
                return Qualities.Sum(item => item.BonusAD);
            }
        }
    }
}