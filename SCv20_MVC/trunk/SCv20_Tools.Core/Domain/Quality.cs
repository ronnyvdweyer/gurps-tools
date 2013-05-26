using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using SCv20_Tools.Core.Domain;


namespace SCv20_Tools.Core.Domain {
    [Serializable]
    public class Quality {

        /// <summary>
        /// Initializes a new instance of the <see cref="Quality"/> class.
        /// </summary>
        public Quality() {

        }


        /// <summary>
        /// ID da Quality.
        /// </summary>
        [Key]
        public virtual int Id {
            get;
            set;
        }


        /// <summary>
        /// Nome da Quality de campanha.
        /// </summary>
        [Required, MaxLength(100)]
        public virtual string Name {
            get;
            set;
        }


        /// <summary>
        /// Descrição da Campaign Quality.
        /// </summary>
        [MaxLength(1000)]
        public virtual string Description {
            get;
            set;
        }


        /// <summary>
        /// Indica se a Quality apenas é válida em Seasons.
        /// </summary>
        [Required]
        public virtual bool IsSeasonsOnly {
            get;
            set;
        }


        /// <summary>
        /// Bonus ou Penalidade de XP de acordo com a quality.
        /// </summary>
        [Required]
        public virtual int BonusXP {
            get;
            set;
        }


        /// <summary>
        /// Bonus ou Penalidade em Action Dices para o GC.
        /// </summary>
        [Required]
        public virtual int BonusAD {
            get;
            set;
        }


        #region -- Navigation Properties -----------------------------------------------------------

        public virtual ICollection<CampaignQuality> CampaignQualities {
            get;
            set;
        }

        public virtual ICollection<MissionQuality> MissionQualities {
            get;
            set;
        }

        #endregion
    }
}
