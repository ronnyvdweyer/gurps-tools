using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCv20.Tools.Core.Domain;
using SCv20.Tools.Core.Domain.CampaignDesign;

namespace SCv20.Tools.Core.Services {
    public class DataService {
        private static DataService _instance;

        private DataService() {
        
        }


        public static DataService GetInstance() {
            lock (typeof(DataService)) {
                if (_instance == null)
                    _instance = new DataService();

                return _instance;
            }
        }


        public IList<KeyValuePair<int, string>> GetAllCareerLevels() {
            var list = new List<KeyValuePair<int, string>>();
            for (int level = 1; level <= 20; level += 2) {
                list.Add(new KeyValuePair<int, string>(level, "Career Level {0}".FormatWith(level)));
            }

            return list;
        }


        public IList<HistoricalConversion> GetAllHistoricalConversions() {
            var repo = Repository<HistoricalConversion>.GetInstance();
            return repo.FindAll().OrderBy(e=>e.Order).ToList();
        }


        public IList<Campaign> GettAllCampaigns() {
            var repo = Repository<Campaign>.GetInstance();
            var data = repo.FindAll().ToList();
            return data;
        }

        
        public IList<Quality> GetAllQualities(bool isSeason) {
            var repo = Repository<Quality>.GetInstance();
            var data = repo.FindBy(e=>e.IsSeasonsOnly == isSeason).ToList();
            
            return data;
        }


        public KeyValuePair<int, string> GetCareerLevel(int level) {
            return GetAllCareerLevels().Where(e => e.Key == level).FirstOrDefault();
        }


        public Campaign GetCampaign(int id) {
            var repo = Repository<Campaign>.GetInstance();
            var data = repo.GetById(id);

            return data;
        }


        public Quality GetQuality(int id) {
            var repo = Repository<Quality>.GetInstance();
            var data = repo.GetById(id);

            return data;
        }


        public Campaign SaveCampaign(Campaign c) {
            var repo1 = Repository<Campaign>.GetInstance();

            if(c.Id > 0)
                c = repo1.Edit(c);
            else
                c = repo1.Create(c);

            repo1.Commit();
            
            return c;
        }
    }
}
