using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCv20_Tools.Core.Domain {

    public class Mission {

        public Mission() {
            CreatedOn = DateTime.Now;
        }

        [Key]
        public int Id {
            get;
            set;
        }

        [Required, MaxLength(100)]
        public string Name {
            get;
            set;
        }

        [MaxLength(10)]
        public string Code {
            get;
            set;
        }

        [Required]
        public int CaliberId {
            get;
            set;
        }

        [MaxLength(150)]
        public string Motivation {
            get;
            set;
        }

        [MaxLength(4000)]
        public string Briefing {
            get;
            set;
        }

        [Required, Range(1, 100)]
        public int TotalPartyLevel {
            get;
            set;
        }

        [Range(-2, +2)]
        public int AdjustedThreatLevel {
            get;
            set;
        }

        public DateTime CreatedOn {
            get;
            set;
        }

        /// <summary>
        /// Calculated: Returns Calculated Party Threat Level.
        /// </summary>
        [NotMapped]
        public int PartyThreatLevel {
            get {
                return Convert.ToInt32(Math.Ceiling((TotalPartyLevel / 5.0)));
            }
        }

        /// <summary>
        /// Calculated: Returns Calculated Mission Threat Level Ajusted.
        /// </summary>
        [NotMapped]
        public int ThreatLevel {
            get {
                return (PartyThreatLevel + AdjustedThreatLevel);
            }
        }

        #region -- Relationships ------------------------------------------------------------------

        [ForeignKey("CaliberId")]
        public virtual Caliber Caliber {
            get;
            set;
        }

        public virtual ICollection<MissionQuality> Qualities {
            get;
            set;
        }

        public virtual ICollection<Scene> Scenes {
            get;
            set;
        }

        #endregion -- Relationships ------------------------------------------------------------------
    }
}