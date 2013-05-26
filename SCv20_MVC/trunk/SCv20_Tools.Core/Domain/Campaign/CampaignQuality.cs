using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCv20_Tools.Core.Domain {

    public class CampaignQuality {

        [Key]
        public int Id {
            get;
            set;
        }

        [ForeignKey("CampaignId")]
        public virtual Campaign Campaign {
            get;
            set;
        }

        [ForeignKey("QualityId")]
        public virtual Quality Quality {
            get;
            set;
        }

        public int CampaignId {
            get;
            set;
        }

        public int QualityId {
            get;
            set;
        }
    }
}