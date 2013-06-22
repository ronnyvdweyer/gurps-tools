using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SCv20_Tools.Core.Domain {
    public class ObjectiveType {
        
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

        [MaxLength(1000)]
        public string Description {
            get;
            set;
        }

        #region -- Navigation Properties -----------------------------------------------------------

        public virtual ICollection<ObjectiveGrade> ObjectiveGrades {
            get;
            set;
        }

        #endregion
    }
}
