using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Web.Models {

    [Serializable]
    public class SceneObjectiveModel {

        public SceneObjectiveModel() {
            this.CriticalTypes = new List<CriticalType>() { new CriticalType(0, "Critical?"), new CriticalType(1, "Yes") };
            this.PlotTypes = new List<PlotType>() { new PlotType(0, "Plot?"), new PlotType(1, "Yes") };
            this.SceneObjectiveTypes = new List<SceneObjectiveType>();
            this.SceneObjectiveGrades = new List<SceneObjectiveGrade>();
            this.Order = 1;
        }

        /// <summary>
        /// Scene Objective ID.
        /// </summary>
        public int ID {
            get;
            set;
        }

        /// <summary>
        /// Scene ID.
        /// </summary>
        public int SceneID {
            get;
            set;
        }

        [Required, MaxLength(100), DisplayName("Description")]
        public string Description {
            get;
            set;
        }

        [Range(1, 5, ErrorMessage = "Please, select an objetive grade."), DisplayName("Grade")]
        public int GradeID {
            get;
            set;
        }

        [Range(1, int.MaxValue, ErrorMessage = "Please, select an objetive type."), DisplayName("Type")]
        public int ObjectiveTypeID {
            get;
            set;
        }

        [DisplayName("Critical?")]
        public int CriticalTypeSelected {
            get;
            set;
        }

        [DisplayName("Plot?")]
        public int PlotTypeSelected {
            get;
            set;
        }

        public int Order {
            get;
            set;
        }

        public string OrderFormatted {
            get;
            private set;
        }

        public List<CriticalType> CriticalTypes {
            get;
            set;
        }

        public List<PlotType> PlotTypes {
            get;
            set;
        }

        public List<SceneObjectiveType> SceneObjectiveTypes {
            get;
            set;
        }

        public List<SceneObjectiveGrade> SceneObjectiveGrades {
            get;
            set;
        }

        public string ObjectiveTypeDescription {
            get;
            set;
        }

        public string ObjectiveTypeXPFormated {
            get;
            set;
        }

        #region -- Nested Specific Classes ---------------------------------------------------------

        public class CriticalType {

            public CriticalType(int value, string text) {
                this.Value = value;
                this.Text = text;
            }

            public int Value {
                get;
                private set;
            }

            public string Text {
                get;
                private set;
            }
        }

        public class PlotType {

            public PlotType(int value, string text) {
                this.Value = value;
                this.Text = text;
            }

            public int Value {
                get;
                private set;
            }

            public string Text {
                get;
                private set;
            }
        }

        public class SceneObjectiveType {

            public int Value {
                get;
                private set;
            }

            public string Text {
                get;
                private set;
            }

            public SceneObjectiveType MapFrom(ObjectiveType entity) {
                if (entity == null) {
                    return this;
                }

                this.Value = entity.Id;
                this.Text = entity.Name;

                return this;
            }

            public static SceneObjectiveType CreateFrom(ObjectiveType entity) {
                var model = new SceneObjectiveType();
                return model.MapFrom(entity);
            }
        }

        public class SceneObjectiveGrade {

            public int Value {
                get;
                private set;
            }

            public string Text {
                get;
                private set;
            }

            public SceneObjectiveGrade MapFrom(Caliber entity) {
                if (entity == null) {
                    return this;
                }

                this.Value = entity.Id;
                this.Text = entity.Value;

                return this;
            }

            public static SceneObjectiveGrade CreateFrom(Caliber entity) {
                var model = new SceneObjectiveGrade();
                return model.MapFrom(entity);
            }
        }

        #endregion -- Nested Specific Classes ---------------------------------------------------------

        public SceneObjectiveModel MapFrom(SceneObjective objective, IList<Caliber> calibers, IList<ObjectiveType> types) {
            if (objective != null) {
                this.ID = objective.Id;
                this.SceneID = objective.SceneId;

                this.Order = objective.Order;
                this.OrderFormatted = objective.OrderFormatted;
                this.Description = objective.Description;

                this.GradeID = objective.Grade.Grade;// .GradeId;           == Caliber
                this.ObjectiveTypeID = objective.Grade.ObjectiveTypeId; //  == TypeId

                this.CriticalTypeSelected = objective.IsCritical ? 1 : 0;
                this.PlotTypeSelected = objective.IsPlot ? 1 : 0;
                this.ObjectiveTypeDescription = objective.Grade.Description;
                this.ObjectiveTypeXPFormated = objective.Grade.ObjectiveGradeXPFormatted;
            }

            if (calibers != null) {
                this.SceneObjectiveGrades = calibers.Select(item => SceneObjectiveGrade.CreateFrom(item)).ToList();
            }

            if (types != null) {
                this.SceneObjectiveTypes = types.Select(item => SceneObjectiveType.CreateFrom(item)).ToList();
            }

            return this;
        }

        public static SceneObjectiveModel CreateFrom(SceneObjective objective, IList<Caliber> calibers, IList<ObjectiveType> types) {
            var model = new SceneObjectiveModel();
            return model.MapFrom(objective, calibers, types);
        }

        public SceneObjective MapToSceneObjectiveEntity() {
            var entity = new SceneObjective();

            entity.Id = this.ID;
            entity.Description = this.Description;
            entity.SceneId = this.SceneID;
            entity.GradeId = this.GradeID;
            entity.IsCritical = this.CriticalTypeSelected == 1 ? true : false;
            entity.IsPlot = this.PlotTypeSelected == 1 ? true : false;
            entity.Description = this.Description;
            entity.ObjectiveTypeID = this.ObjectiveTypeID;
            entity.Order = this.Order;

            return entity;
        }
    }
}