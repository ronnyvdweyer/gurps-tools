using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SCv20.Tools.Core.Domain;
using SCv20.Tools.Core.Domain.CampaignDesign;

namespace SCv20.Tools.Core.DataContext {
    public class CampaignMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Campaign> {
        public CampaignMap() {
            //this.ToTable("Campaign_Quality");
            //this.HasKey(m => m.Id);
        }
    }
}
