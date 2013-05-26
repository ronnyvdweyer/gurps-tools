using System;
using System.Collections.Generic;
using System.Linq;
using SCv20_Tools.Core.Data;
using SCv20_Tools.Core.Domain;

namespace SCv20_Tools.Core.Services {

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

        public IList<Campaign> GetAllCampaigns() {
            var repo = Repository<Campaign>.GetInstance();
            var data = repo.FindAll().ToList();
            return data;
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
            return repo.FindAll().OrderBy(e => e.Order).ToList();
        }

        public IList<Quality> GetAllQualities(bool? isSeason) {
            var repo = Repository<Quality>.GetInstance();
            var data = repo.FindAll();

            if (isSeason.HasValue) {
                data = data.Where(e => e.IsSeasonsOnly == isSeason);
            }

            return data.OrderBy(e => e.Name).ToList();

            //var vListVendors = (from eee in vListSupplier
            //                    where !(from ppp in vListCustomer select ppp.SupplierID).ToList().Contains(eee.SupplierID)
            //                    select eee).ToList();
        }

        public IList<Quality> GetAllQualitiesExcludingExistingFromCampaign(int campaignId) {
            var repoQuality = Repository<Quality>.GetInstance();
            var repoCampaign = Repository<Campaign>.GetInstance();

            var campaign = repoCampaign.GetById(campaignId);

            if (campaign == null) {
                return repoQuality.FindAll().OrderBy(e => e.Name).ToList();
            }
            else {
                var aaa = campaign.Qualities.ToList();    // Customers
                var bbb = repoQuality.FindAll().ToList();               // Orders

                var query = from c in bbb
                            where !(from o in aaa select o.QualityId).Contains(c.Id)
                            select c;

                var r = query.OrderBy(e => e.Name).ToList();

                //var availQualities = (from Q in repoQuality.FindAll()
                //                      where !(from CQ in campaignQualities
                //                              select CQ.Id).Contains(Q.Id)
                //                      select Q)/*.ToList()*/;

                ///var rrr = availQualities.ToList();

                return r;

                //availQualities;
            }
        }

        public Campaign GetCampaign(int id) {
            var repo = Repository<Campaign>.GetInstance();
            var data = repo.GetById(id);

            return data;
        }

        public KeyValuePair<int, string> GetCareerLevel(int level) {
            return GetAllCareerLevels().Where(e => e.Key == level).FirstOrDefault();
        }

        public Quality GetQuality(int id) {
            var repo = Repository<Quality>.GetInstance();
            var data = repo.GetById(id);

            return data;
        }

        public Campaign SaveCampaign(Campaign c) {
            var repo1 = Repository<Campaign>.GetInstance();

            if (c.Id > 0)
                c = repo1.Edit(c);
            else
                c = repo1.Create(c);

            repo1.Commit();

            return c;
        }

        public IList<Mission> GetAllMissions() {
            var repo = Repository<Mission>.GetInstance();
            var data = repo.FindAll().ToList();
            return data;
        }

        public IList<Caliber> GetAllCalibers() {
            var repo = Repository<Caliber>.GetInstance();
            var data = repo.FindAll().ToList();
            return data;
        }
    }
}