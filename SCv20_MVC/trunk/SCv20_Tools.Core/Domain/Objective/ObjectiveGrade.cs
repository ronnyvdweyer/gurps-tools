using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCv20_Tools.Core.Domain {

    public class ObjectiveGrade {
        [Key]
        public int Id {
            get;
            set;
        }

        [Required, Range(1, 5)]
        public int Grade {
            get;
            set;
        }

        [Required, MaxLength(1000)]
        public string Description {
            get;
            set;
        }

        [Required]
        public int ObjectiveTypeId {
            get;
            set;
        }

        [ForeignKey("ObjectiveTypeId")]
        public virtual ObjectiveType ObjectiveType {
            get;
            set;
        }

        /// <summary>
        /// Calculated: Returns Objective XP based on objective Grade.
        /// </summary>
        [NotMapped]
        public int ObjectiveGradeXP {
            get {
                return Grade * 25;
            }
        }

        /// <summary>
        /// Calculated: Returns Objective XP Formatted based on objective Grade.
        /// </summary>
        [NotMapped]
        public string ObjectiveGradeXPFormatted {
            get {
                return string.Format("+{0} XP", Grade * 25);
            }
        }

        #region -- Navigation Properties -----------------------------------------------------------

        public virtual ICollection<SceneObjective> SceneObjectives {
            get;
            set;
        }

        #endregion
    }
}