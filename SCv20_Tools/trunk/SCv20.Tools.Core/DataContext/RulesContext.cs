using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SCv20.Tools.Core.Domain;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SCv20.Tools.Core.DataContext {
    public class RulesContext : DbContext {

        public RulesContext() : base("DEFAULT") {
            Database.SetInitializer(new RulesContextInitializer());
            Database.Initialize(true);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<Caliber>   Caliber { get; set; }


        public virtual DbSet<Asset>     Assets  { get; set; }


        public virtual DbSet<Quality>   Quality { get; set; }


        public virtual DbSet<Dummy>     Dummy   { get; set; }

    }
}
