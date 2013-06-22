using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCv20_Tools.Web.Models {
    public class MissionListingModel {
        public MissionListingModel() {
            Missions = new List<MissionSummaryModel>();
        }

        public List<MissionSummaryModel> Missions {
            get;
            set;
        }
    }
}