using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Core.Data {

    public class DataContext : DbContext {
        private static DataContext _instance;

        public DataContext() : base("DEFAULT") {
            Database.SetInitializer(new DataContextInitializer());
            Database.Initialize(true);
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

        #endregion -- Data Sets ------------------------------------------------------------
    }
}