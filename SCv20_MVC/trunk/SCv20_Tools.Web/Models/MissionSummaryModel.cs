using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Web.Models {

    public class MissionSummaryModel {

        public MissionSummaryModel() {
            this.AdjustedThreatLevelList = new List<MissionSummaryModel.ThreatAdjust> {
                new ThreatAdjust {  Value = -2, Description = "Very Easy" },
                new ThreatAdjust {  Value = -1, Description = "Easy"      },
                new ThreatAdjust {  Value =  0, Description = "Average"   },
                new ThreatAdjust {  Value = +1, Description = "Hard"      },
                new ThreatAdjust {  Value = +2, Description = "Very Hard" },
            };
        }

        /// <summary>
        /// Gets or sets the MissionId.
        /// </summary>
        public int Id {
            get;
            set;
        }

        [Range(-2, +2)]
        public int AdjustedThreatLevel {
            get;
            set;
        }

        [Required, MaxLength(100), Display(Name = "Mission Name")]
        public string Name {
            get;
            set;
        }

        [MaxLength(10)]
        public string Code {
            get;
            set;
        }

        [MaxLength(150)]
        public string Motivation {
            get;
            set;
        }

        [MaxLength(4000), AllowHtml]
        public string Briefing {
            get;
            set;
        }

        [Required]
        public int CaliberId {
            get;
            set;
        }

        [Range(1, 100), Display(Name = "Total Party Level")]
        public int TotalPartyLevel {
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

        public string CaliberFormatted {
            get;
            set;
        }

        [Display(Name = "Caliber")]
        public List<CaliberModel> CaliberList {
            get;
            set;
        }

        [Display(Name = "Threat Adjust")]
        public List<MissionSummaryModel.ThreatAdjust> AdjustedThreatLevelList {
            get;
            set;
        }

        public Mission ToEntity() {
            var entity = new Mission {
                Id = this.Id,
                AdjustedThreatLevel = this.AdjustedThreatLevel,
                Briefing = this.Briefing,
                CaliberId = this.CaliberId,
                Code = this.Code,
                Motivation = this.Motivation,
                Name = this.Name,
                TotalPartyLevel = this.TotalPartyLevel
            };

            return entity;
        }

        public MissionSummaryModel MapFrom(Mission entity) {
            if (entity == null)
                return new MissionSummaryModel();

            this.AdjustedThreatLevel = entity.AdjustedThreatLevel;
            this.Briefing = entity.Briefing;
            this.CaliberId = entity.CaliberId;
            this.Code = entity.Code;
            this.Id = entity.Id;
            this.Motivation = entity.Motivation;
            this.Name = entity.Name;
            this.TotalPartyLevel = entity.TotalPartyLevel;

            return this;
        }

        public class ThreatAdjust {

            public int Value { get; set; }

            public string Description { get; set; }

            public string ValueFormatted {
                get {
                    if (Value < 0)
                        return string.Format("{0} ({1})", Value, Description);
                    else
                        return string.Format("+{0} ({1})", Value, Description);
                }
            }
        }
    }
}