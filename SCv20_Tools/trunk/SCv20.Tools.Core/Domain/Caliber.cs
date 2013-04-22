using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SCv20.Tools.Core.Domain {
    [Serializable]
    public class Caliber {

        /// <summary>
        /// Initializes a new instance of the <see cref="Caliber"/> class.
        /// </summary>
        public Caliber() { 
        
        }


        /// <summary>
        /// ID do Caliber
        /// </summary>
        [Key]
        public int Id { 
            get; 
            set; 
        }


        /// <summary>
        /// Valor em numerais romanos do Caliber.
        /// </summary>
        [MaxLength(3)]
        public string Value {
            get;
            set;
        }


        /// <summary>
        /// Quantidade de Reputação típica para uma missão do Caliber especificado.
        /// </summary>
        [Range(2, 20)]
        public int Reputation {
            get;
            set;
        }


        /// <summary>
        /// Valor de Networth típico para uma missão do Caliber especificado.
        /// </summary>
        public decimal Networth {
            get;
            set;
        }


        /// <summary>
        /// Quantidade de Reputação formatada. Ex: 10 => +10.
        /// </summary>
        [NotMapped]
        public string ReputationFormated {
            get {
                return "+{0}".FormatWith(Reputation);
            }
        }

        
        /// <summary>
        /// Valor de Networth formatado. Ex: 750000 => $750.000.
        /// </summary>
        [NotMapped]
        public string NetworthFormated {
            get {
                //var format = Networth.ToString("$ #,0.##").Replace(",", ".");
                var format = String.Format("{0:C0}", NetworthFormated);
                return format;
            }
        }


        /// <summary>
        /// Retorna representação textual do Caliber representado. Ex.: Caliber III
        /// </summary>
        public override string ToString() {
            return "Caliber {0}".FormatWith(this.Value);
        }
    }
}
