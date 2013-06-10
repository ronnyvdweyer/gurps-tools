using System.ComponentModel.DataAnnotations;
using System.Linq;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Web.Models {

    public class ObjectiveModel {

        public int ID {
            get;
            set;
        }

        [Required]
        public int TypeID {
            get;
            set;
        }

        [Range(1, 5)]
        public int GradeID {
            get;
            set;
        }

        [Required]
        public string Description {
            get;
            set;
        }

        public int GradeXP {
            get;
            private set;
        }

        public string GradeXPFormated {
            get;
            private set;
        }

        public ObjectiveModel MapFrom(ObjectiveGrade entity) {
            if (entity == null)
                return this;

            this.ID = entity.Id;
            this.TypeID = entity.ObjectiveTypeId;
            this.GradeID = entity.Grade;
            this.Description = entity.Description;
            this.GradeXP = entity.ObjectiveGradeXP;
            this.GradeXPFormated = entity.ObjectiveGradeXPFormatted;

            return this;
        }

        public static ObjectiveModel CreateFrom(ObjectiveGrade entity) {
            var model = new ObjectiveModel();
            return model.MapFrom(entity);
        } 
    }
}