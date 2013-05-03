using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Core.Data {

    public class DataContext : DbContext {
        private static DataContext _instance;

        private DataContext() : base("DEFAULT") {
            Database.SetInitializer(new DataContextInitializer());
            Database.Initialize(true);
        }

        public static DataContext BuildContext() {

            return new DataContext();

            //lock (typeof(DataContext)) {
            //    if (_instance == null)
            //        _instance = new DataContext();

            //    return _instance;
            //}
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Configurations.Add(new CampaignMap());
            //http://stackoverflow.com/questions/8136026/unidirectional-many-to-many-realtionship-with-code-first-entity-framework

            modelBuilder.Entity<Campaign>().HasMany(m => m.Qualities).WithMany();

            base.OnModelCreating(modelBuilder);
        }

        #region -- Data Sets ------------------------------------------------------------

        public virtual DbSet<Caliber> Caliber {
            get;
            set;
        }

        public virtual DbSet<Asset> Assets {
            get;
            set;
        }

        public virtual DbSet<Quality> Quality {
            get;
            set;
        }

        public virtual DbSet<HistoricalConversion> HistoricalConversion {
            get;
            set;
        }

        public virtual DbSet<Campaign> Campaign {
            get;
            set;
        }

        #endregion -- Data Sets ------------------------------------------------------------
    }
}