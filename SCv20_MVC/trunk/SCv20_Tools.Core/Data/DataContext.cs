using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Core.Data {

    public class DataContext : DbContext {
        private static DataContext _instance;

        public DataContext() : base("DEFAULT") {
            try {

                Database.SetInitializer(new DataContextInitializer());
                Database.Initialize(true);
            }
            catch (DataException ex) {
                if (ex.InnerException != null && ex.InnerException is DbEntityValidationException) {
                    var msg = DataContextInitializer.BuildValidationMessage(ex.InnerException as DbEntityValidationException);
                    throw new DbEntityValidationException("Entity Validation Failed - Errors Follow in " + msg);
                }
                throw ex;
            }
        }

        public static DataContext BuildContext() {

            //return new DataContext();

            lock (typeof(DataContext)) {
                if (_instance == null)
                    _instance = new DataContext();

                return _instance;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Configurations.Add(new CampaignMap());
            //http://stackoverflow.com/questions/8136026/unidirectional-many-to-many-realtionship-with-code-first-entity-framework

            //modelBuilder.Entity<Campaign>().HasMany(m => m.Qualities).WithMany();

            //modelBuilder.Entity<SceneObjective>()
                //.HasRequired(i => i.Caliber).WithMany().WillCascadeOnDelete(false);//.WithMany().Map(x => x.MapKey("XX")).WillCascadeOnDelete(false);
                //.WithRequiredPrincipal(i=>i.Id).WillCascadeOnDelete(false);
                //.HasForeignKey(t => t.Id)
                //.WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }

        #region -- Data Sets ------------------------------------------------------------

        public virtual DbSet<Caliber> Calibers {
            get;
            set;
        }

        public virtual DbSet<Asset> Assets {
            get;
            set;
        }

        public virtual DbSet<Quality> Qualities {
            get;
            set;
        }

        public virtual DbSet<ObjectiveType> ObjectiveTypes {
            get;
            set;
        }

        public virtual DbSet<HistoricalConversion> HistoricalConversions {
            get;
            set;
        }



        public virtual DbSet<Campaign> Campaigns {
            get;
            set;
        }

        public virtual DbSet<CampaignQuality> CampaignQualities {
            get;
            set;
        }

        public virtual DbSet<Mission> Missions {
            get;
            set;
        }

        public virtual DbSet<MissionQuality> MissionQualities {
            get;
            set;
        }

        public virtual DbSet<Scene> Scenes {
            get;
            set;
        }

        public virtual DbSet<SceneObjective> SceneObjectives {
            get;
            set;
        }

        #endregion -- Data Sets ------------------------------------------------------------
    }
}