using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SCv20_Tools.Core.Data;

namespace SCv20_Tools.Core.Domain {

    public class Scene {

        [Key]
        public int ID {
            get;
            set;
        }

        public bool IsDramatic {
            get;
            set;
        }

        [Required]
        public int MissionID {
            get;
            set;
        }

        [MaxLength(100)]
        public string Name {
            get;
            set;
        }

        [MaxLength]
        public string Description {
            get;
            set;
        }

        [Range(1, 9)]
        public int Order {
            get;
            set;
        }

        [IgnoreOnUpdate()]
        public DateTime CreatedOn {
            get;
            set;
        }

        #region -- Navigation Properties -----------------------------------------------------------

        [ForeignKey("MissionID")]
        public virtual Mission Mission {
            get;
            set;
        }

        public virtual ICollection<SceneObjective> Objectives {
            get;
            set;
        }

        #endregion -- Navigation Properties -----------------------------------------------------------

        /// <summary>
        /// Calculated: Returns Order formated.
        /// </summary>
        [NotMapped]
        public string OrderFormated {
            get {
                return string.Format("#{0}", Order);
            }
        }
    }
}