using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Reflection;
using System.IO;
using SCv20.Tools.Core.Services;
using SCv20.Tools.Core.Domain;

namespace SCv20.Tools.Core.DataContext {
    internal class RulesContextInitializer : DropCreateDatabaseIfModelChanges<RulesContext> {

        protected override void Seed(RulesContext context) {
            LoadCaliber().ForEach(c => context.Caliber.Add(c));
            LoadQualities().ForEach(c => context.Quality.Add(c));
        }


        private static List<Caliber> LoadCaliber() {
            var list = new List<Caliber> { 
                new Caliber { Id = 1, Value = "I",   Reputation =  2,  Networth =  100000M },
                new Caliber { Id = 2, Value = "II",  Reputation =  5,  Networth =  250000M },
                new Caliber { Id = 3, Value = "III", Reputation = 10,  Networth =  500000M },
                new Caliber { Id = 4, Value = "IV",  Reputation = 15,  Networth =  750000M },
                new Caliber { Id = 5, Value = "V",   Reputation = 20,  Networth = 1000000M }
            };

            return list;
        }


        private static List<Quality> LoadQualities() {
            var svc  = SerializationService.GetInstance();
            var file = GetSeedResource("SCv20.Tools.Core.DataContext.Seed.Qualities.json.js");
            var json = svc.Deserialize(file);
            var list = new List<Quality>();

            foreach (var x in json.data) {
                var e = new Quality { BonusAD = x.ad, BonusXP = x.xp, Description = x.description, IsSeasonsOnly = x.season, Name = x.name, Dummy = null };
                list.Add(e);
            }

            return list;
        }
       

        private static string GetSeedResource(string resourceID) {
            var assembly = Assembly.GetExecutingAssembly();
            try {
                var stream = new StreamReader(assembly.GetManifestResourceStream(resourceID));
                var contents = stream.ReadToEnd();
                stream.Close();
                stream.Dispose();
                return contents;
            }
            catch (ArgumentNullException ex) {
                throw new InvalidOperationException("Requested Resource [{0}] not found in [{1}].".FormatWith(resourceID, assembly.FullName), ex);
            }
        }
    }
}
