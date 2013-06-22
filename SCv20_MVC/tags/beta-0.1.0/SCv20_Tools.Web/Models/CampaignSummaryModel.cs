using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Web.Models {
    public class CampaignSummaryModel {

        public int Id { 
            get; 
            private set; 
        }

        public string Name {
            get;
            private set;
        }

        public string Concept {
            get;
            private set;
        }


        public static CampaignSummaryModel MapFrom(Campaign entity) {
            return null;
        }
    }
}