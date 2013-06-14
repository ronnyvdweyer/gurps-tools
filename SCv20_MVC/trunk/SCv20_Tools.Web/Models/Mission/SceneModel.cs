using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Web.Models {

    public class SceneModel {

        public SceneModel() {
            this.SceneTypes = new List<SceneType>() { new SceneType(0, "Normal"), new SceneType(1, "Dramatic") };
            this.SceneObjectivesID = new List<Int32>();
            this.Order = 1;
        }

        /// <summary>
        /// Scene ID.
        /// </summary>
        public int ID {
            get;
            set;
        }

        public int MissionID {
            get;
            set;
        }

        [Required, MaxLength(100)]
        public string Name {
            get;
            set;
        }

        [Required, MaxLength, AllowHtml]
        public string Description {
            get;
            set;
        }

        [DisplayName("Scene Type")]
        public int SceneTypeID {
            get;
            set;
        }

        [Range(1, 9), DisplayName("Order")]
        public int Order {
            get;
            set;
        }

        [DisplayName("Created On"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedOn {
            get;
            set;
        }

        public List<SceneType> SceneTypes {
            get;
            set;
        }

        public List<Int32> SceneObjectivesID {
            get;
            set;
        }

        public class SceneType {

            public SceneType(int value, string text) {
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

        public SceneModel MapFrom(Scene entity) {
            if (entity == null) {
                return this;
            }

            this.ID = entity.ID;
            this.MissionID = entity.MissionID;
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.SceneTypeID = entity.IsDramatic ? 1 : 0;
            this.Order = entity.Order;
            this.CreatedOn = entity.CreatedOn;
            this.SceneObjectivesID = entity.Objectives.OrderBy(item => item.Order).Select(item => item.Id).ToList();

            if (this.SceneObjectivesID.Count == 0) {
                this.SceneObjectivesID.Add(0);
            }

            return this;
        }

        public Scene MapToSceneEntity() {
            var entity = new Scene();

            entity.ID = this.ID;
            entity.MissionID = this.MissionID;
            entity.Name = this.Name;
            entity.Description = this.Description;
            entity.IsDramatic = SceneTypeID == 1 ? true : false;
            entity.Order = this.Order;

            return entity;
        }

        public static SceneModel CreateFrom(Scene entity) {
            var model = new SceneModel();
            return model.MapFrom(entity);
        }
    }
}