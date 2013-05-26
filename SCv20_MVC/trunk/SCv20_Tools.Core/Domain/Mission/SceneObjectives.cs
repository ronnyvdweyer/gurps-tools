using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCv20_Tools.Core.Domain {

    public class SceneObjective {

        [Key]
        public int Id {
            get;
            set;
        }

        public int SceneId {
            get;
            set;
        }

        public int GradeId {
            get;
            set;
        }

        public bool IsCritical {
            get;
            set;
        }

        public bool IsPlot {
            get;
            set;
        }

        [MaxLength(1000)]
        public string Description {
            get;
            set;
        }

        /// <summary>
        /// Calculated: Returns Objective XP based on objective Type and Grade.
        /// </summary>
        [NotMapped]
        public int ObjectiveXP {
            get {
                if (Grade != null)
                    return Grade.ObjectiveGradeXP;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Calculated: Returns Objective XP Formatted based on objective Type and Grade.
        /// </summary>
        [NotMapped]
        public string ObjectiveXPFormatted {
            get {
                if (Grade != null)
                    return Grade.ObjectiveGradeXPFormatted;
                else
                    return string.Empty;
            }
        }

        #region -- Navigation Properties -----------------------------------------------------------

        [Required, ForeignKey("SceneId")]
        public virtual Scene Scene {
            get;
            set;
        }

        [Required, ForeignKey("GradeId")]
        public virtual ObjectiveGrade Grade {
            get;
            set;
        }

        #endregion -- Navigation Properties -----------------------------------------------------------
    }
}