using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCv20_Tools.Core.Domain {

    public class CampaignMission {

        [Key]
        public int Id {
            get;
            set;
        }

        public int CampaignId {
            get;
            set;
        }

        public int MissionId {
            get;
            set;
        }

        #region -- Relationships --------------------------------------------------------

        [ForeignKey("CampaignId")]
        public virtual Campaign Campaign {
            get;
            set;
        }

        [ForeignKey("MissionId")]
        public virtual Mission Mission {
            get;
            set;
        }

        #endregion -- Relationships --------------------------------------------------------
    }
}