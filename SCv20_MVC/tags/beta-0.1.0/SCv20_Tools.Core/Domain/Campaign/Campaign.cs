using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCv20_Tools.Core.Domain {

    [Serializable]
    public class Campaign {

        public Campaign() {
            YearId = 1;
            BaseReputation = 2;
            BaseNetWorth = 1000000M;
            CreatedOn = DateTime.Now;
            Qualities = new List<CampaignQuality>();
        }

        [Key]
        public virtual int Id {
            get;
            set;
        }

        [Range(2, 20)]
        public virtual int BaseReputation {
            get;
            set;
        }

        [Range(100000, 2000000)]
        public virtual decimal BaseNetWorth {
            get;
            set;
        }

        [Range(0, 20)]
        public virtual int StartingLevel {
            get;
            set;
        }

        [MaxLength(10)]
        public virtual string Code {
            get;
            set;
        }

        [MaxLength(100)]
        public virtual string Name {
            get;
            set;
        }

        public int YearId {
            get;
            set;
        }

        [MaxLength(100)]
        public virtual string YearDetails {
            get;
            set;
        }

        [MaxLength(200)]
        public virtual string Concept {
            get;
            set;
        }

        [MaxLength(1000)]
        public virtual string Summary {
            get;
            set;
        }

        public virtual DateTime CreatedOn {
            get;
            set;
        }

        [NotMapped]
        public string BaseNetWorthFormatted {
            get {
                //var format = BaseNetWorth.ToString("$ #,0.##").Replace(",", ".");
                var format = String.Format("{0:C0}", BaseNetWorth);
                return format;
            }
        }

        #region -- Relationships --------------------------------------------------------

        [ForeignKey("YearId")]
        public virtual HistoricalConversion Year {
            get;
            set;
        }

        public virtual ICollection<CampaignQuality> Qualities {
            get;
            set;
        }

        public virtual ICollection<CampaignMission> Missions {
            get;
            set;
        }

        #endregion -- Relationships --------------------------------------------------------
    }
}