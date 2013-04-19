using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCv20.Tools.Core.Domain.CampaignDesign {
    public class CampaignInfo {
        public int Id {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }

        public int BaseXP {
            get;
            set;
        }
        //TODO: Adicionar outras informações da Campanha
    }
}
