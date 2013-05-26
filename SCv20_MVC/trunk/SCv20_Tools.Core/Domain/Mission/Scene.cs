using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCv20_Tools.Core.Domain {

    public class Scene {

        [Key]
        public int Id {
            get;
            set;
        }

        public bool IsDramatic {
            get;
            set;
        }

        public int MissionId {
            get;
            set;
        }

        [MaxLength]
        public string Description {
            get;
            set;
        }

        public virtual ICollection<SceneObjective> Objectives {
            get;
            set;
        }

        #region -- Navigation Properties -----------------------------------------------------------

        [ForeignKey("MissionId")]
        public virtual Mission Mission {
            get;
            set;
        }

        #endregion -- Navigation Properties -----------------------------------------------------------
    }
}