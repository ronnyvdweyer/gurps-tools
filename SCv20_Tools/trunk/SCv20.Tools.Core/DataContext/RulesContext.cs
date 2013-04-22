using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SCv20.Tools.Core.Domain;
using SCv20.Tools.Core.Domain.CampaignDesign;


namespace SCv20.Tools.Core.DataContext {
    public class RulesContext : DbContext {
        private static RulesContext _instance;


        private RulesContext(): base("DEFAULT") {
            Database.SetInitializer(new RulesContextInitializer());
            Database.Initialize(true);
        }


        public static RulesContext GetInstance() {
            lock (typeof(RulesContext)) {
                if (_instance == null)
                    _instance = new RulesContext();

                return _instance;
            }
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


        #endregion
    }
}
