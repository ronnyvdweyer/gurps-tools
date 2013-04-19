using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCv20.Tools.Core.Domain.CampaignDesign {
    [Serializable]
    public class Campaign {
        //public Campaign() {
        //    Info = new CampaignInfo();
        //}

        public virtual int Id {
            get;
            set;
        }

        public virtual int BaseReputation {
            get;
            set;
        }

        public virtual decimal BaseNetWorth {
            get;
            set;
        }

        public virtual int StartingLevel {
            get;
            set;
        }

        public virtual string Code {
            get;
            set;
        }

        public virtual CampaignInfo Info {
            get;
            set;
        } 

    }
}
