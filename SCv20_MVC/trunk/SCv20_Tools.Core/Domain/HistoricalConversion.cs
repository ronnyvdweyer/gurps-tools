using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCv20_Tools.Core {
    public class HistoricalConversion {
        [Key]
        public int Id {
            get;
            set;
        }

        [MaxLength(50)]
        public string Year {
            get;
            set;
        }

        public int Order {
            get;
            set;
        }

        [Range(0.01, 2.00)]
        public decimal Modifier {
            get;
            set;
        }

        [NotMapped]
        public string ModifierFormated {
            get {
                return "x {0}".FormatWith(Modifier);
            }
        }
    }
}
