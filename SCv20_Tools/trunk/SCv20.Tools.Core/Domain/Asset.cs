using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SCv20.Tools.Core.Domain {
    [Serializable]
    public class Asset {

        /// <summary>
        /// Initializes a new instance of the <see cref="Asset"/> class.
        /// </summary>
        public Asset() {
            CreatedOn = DateTime.Now;
        }


        /// <summary>
        /// ID do Asset.
        /// </summary>
        [Key]
        public virtual int Id {
            get;
            set;
        }
        

        /// <summary>
        /// Asset pai ao que este assset deverá pertencer.
        /// </summary>
        public virtual int? ParentId {
            get;
            set;
        }


        /// <summary>
        /// Nome do Asset
        /// </summary>
        [MaxLength(100)]
        public virtual string Name {
            get;
            set;
        }


        /// <summary>
        /// Descrição do Asset
        /// </summary>
        [MaxLength(1000)]
        public virtual string Description {
            get;
            set;
        }


        /// <summary>
        /// Dados serializados do Asset.
        /// </summary>
        [MaxLength, Required]
        public virtual string Data {
            get;
            internal set;
        }


        /// <summary>
        /// Data de Criação do Registro
        /// </summary>
        public virtual DateTime CreatedOn {
            get;
            set;
        }


        /// <summary>
        /// Tipo do asset.
        /// </summary>
        [Required]
        public virtual AssetType Type {
            get;
            set;
        }
    }


    /// <summary>
    /// Enum contendo os tipos de assets permitidos.
    /// </summary>
    public enum AssetType {
        Campaign,
        Mission,
        Npc
    }
}


//[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed)]