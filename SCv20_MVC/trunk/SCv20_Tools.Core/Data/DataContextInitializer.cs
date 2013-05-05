using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Reflection;
using SCv20_Tools.Core.Domain;
using SCv20_Tools.Core.Services;
using System.Linq;
using System.Linq.Expressions;

namespace SCv20_Tools.Core.Data {

    internal class DataContextInitializer : DropCreateDatabaseIfModelChanges<DataContext> {
        protected override void Seed(DataContext context) {
            LoadCaliber().ForEach(c => context.Calibers.Add(c));
            LoadQualities().ForEach(c => context.Qualities.Add(c));
            LoadHistoricalConversion().ForEach(c => context.HistoricalConversions.Add(c));
            context.Campaigns.Add(CreateSampleCampaign(context));
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
            var svc = SerializationService.GetInstance();
            var file = GetSeedResource("SCv20_Tools.Core.Data.Seed.Qualities.json.js");
            var json = svc.Deserialize(file);
            var list = new List<Quality>();

            foreach (var x in json.data) {
                var e = new Quality { BonusAD = x.ad, BonusXP = x.xp, Description = x.description, IsSeasonsOnly = x.season, Name = x.name, Dummy = null };
                list.Add(e);
            }

            return list;
        }

        private static List<HistoricalConversion> LoadHistoricalConversion() {
            var list = new List<HistoricalConversion> {
                new HistoricalConversion { Id = 1,  Year = "1875-1900",         Modifier = 0.010M,  Order = 11 },
                new HistoricalConversion { Id = 2,  Year = "1901-1909",         Modifier = 0.025M,  Order = 10 },
                new HistoricalConversion { Id = 3,  Year = "1910-1919",         Modifier = 0.030M,  Order = 09 },
                new HistoricalConversion { Id = 4,  Year = "1920-1929",         Modifier = 0.050M,  Order = 08 },
                new HistoricalConversion { Id = 5,  Year = "1930-1949",         Modifier = 0.060M,  Order = 07 },
                new HistoricalConversion { Id = 6,  Year = "1950-1959",         Modifier = 0.100M,  Order = 06 },
                new HistoricalConversion { Id = 7,  Year = "1960-1969",         Modifier = 0.200M,  Order = 05 },
                new HistoricalConversion { Id = 8,  Year = "1970-1979",         Modifier = 0.300M,  Order = 04 },
                new HistoricalConversion { Id = 9,  Year = "1980-1989",         Modifier = 0.600M,  Order = 03 },
                new HistoricalConversion { Id = 10, Year = "1990-Present Day",  Modifier = 1.000M,  Order = 01 },
                new HistoricalConversion { Id = 11, Year = "2020-Near Future",  Modifier = 1.000M,  Order = 02 }
            };

            return list;
        }

        private static Campaign CreateSampleCampaign(DataContext ctx) {
            var c = new Campaign {
                BaseNetWorth = 1000000,
                BaseReputation = 2,
                Code = "GITS-00001",
                Concept = "Triller futurista de espionagem evolvendo a existência humana com relação à existência artificial.",
                CreatedOn = DateTime.Now,
                Name = "Ghost in the Shell: Public Security SECTION 9 - The First GIG",
                StartingLevel = 10,
                Summary = "n/a",
                YearId = 11,
                YearDetails = "Futuro alternativo ambientado no ano de 2028.",
                Qualities = ctx.Set<Quality>().Where(q => q.Id == 1).ToList()
            };
            return c;
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