using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCv20_Tools.Web.Areas.Api.Models {
    public class CampaignModel {
        public int id { get; set; }

        public string name { get; set; }

        public int startitngLevel { get; set; }

        public string summary { get; set; }

        public string yearDetails { get; set; }

        public int yearId { get; set; }

    }
}