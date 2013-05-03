using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using SCv20_Tools.Core.Domain;


namespace SCv20_Tools.Core.Data {
    public class CampaignMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Campaign> {
        public CampaignMap() {
            //this.ToTable("Campaign_Quality");
            //this.HasKey(m => m.Id);
        }
    }
}
