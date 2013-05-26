using System.Collections.Generic;
using SCv20_Tools.Core.Domain;
namespace SCv20_Tools.Web.Models {

    public class MissionSummaryModel {

        public int MissionId {
            get;
            set;
        }

        public int AdjustedThreatLevel {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }

        public string Code {
            get;
            set;
        }

        public string Caliber {
            get;
            set;
        }

        public string Motivation {
            get;
            set;
        }

        public string Briefing {
            get;
            set;
        }

        public int CaliberId {
            get;
            set;
        }

        public string AdjustedThreatLevelFormatted {
            get {
                if (AdjustedThreatLevel > 0)
                    return string.Format("+{0}", AdjustedThreatLevel);
                else
                    return string.Format("-{0}", AdjustedThreatLevel);
            }
        }

        public int SelectedCaliberId { get; set; }


        public List<CaliberModel> CaliberList { get; set; }
    }
}