using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCv20_Tools.Core.Domain {

    public class MissionQuality {

        [Key]
        public int Id {
            get;
            set;
        }

        public int MissionId {
            get;
            set;
        }

        public int QualityId {
            get;
            set;
        }

        #region -- Relationships ------------------------------------------------------------------

        [ForeignKey("MissionId")]
        public virtual Mission Mission {
            get;
            set;
        }

        [ForeignKey("QualityId")]
        public virtual Quality Quality {
            get;
            set;
        }

        #endregion -- Relationships ------------------------------------------------------------------
    }
}