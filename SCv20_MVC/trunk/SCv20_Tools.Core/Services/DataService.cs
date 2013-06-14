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

        public void AddMissionQuality(int missionid, int qualityid) {
            var repo = Repository<MissionQuality>.GetInstance();
            repo.Create(new MissionQuality { MissionId = missionid, QualityId = qualityid });
            repo.Commit();
        }

        public IList<Caliber> GetAllCalibers() {
            var repo = Repository<Caliber>.GetInstance();
            var data = repo.FindAll().ToList();
            return data;
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

        public List<Quality> GetAllMissionAvaliableQualities(int? missionid) {
            var repoAll = Repository<Quality>.GetInstance();
            var repoSub = Repository<MissionQuality>.GetInstance();

            var all = repoAll.FindBy(e => e.IsSeasonsOnly == false);
            var sub = repoSub.FindBy(e => e.MissionId == missionid).Select(e => e.Quality);
            var tmp = all.ToList().Except(sub.ToList());

            return tmp.ToList().OrderBy(e => e.Name).ToList();
        }

        public List<Quality> GetAllMissionQualities(int? missionid) {
            var repo = Repository<MissionQuality>.GetInstance();
            var data = repo.FindAll();
            var query = data.Where(e => e.MissionId == missionid).Select(r => r.Quality);

            return query.OrderBy(e => e.Name).ToList();
        }

        public IList<Mission> GetAllMissions() {
            var repo = Repository<Mission>.GetInstance();
            var data = repo.FindAll().ToList();
            return data;
        }

        public IList<ObjectiveType> GetAllObjectiveTypes() {
            var repo = Repository<ObjectiveType>.GetInstance();
            return repo.FindAll().OrderBy(e => e.Name).ToList();
        }

        public IList<SceneObjective> GetAllSceneObjectives(int sceneid) {
            if (sceneid == 0)
                throw new ArgumentNullException("sceneid");

            var repo = Repository<SceneObjective>.GetInstance();
            return repo.FindBy(e => e.SceneId == sceneid).OrderBy(e => e.Order).ToList();
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

        public IList<Scene> GetAllScenes(int missionid) {
            var repo = Repository<Scene>.GetInstance();
            var data = repo.FindBy(e => e.MissionID == missionid).OrderBy(e => e.Order).ToList();
            return data;
        }

        public Campaign GetCampaign(int id) {
            var repo = Repository<Campaign>.GetInstance();
            var data = repo.GetById(id);

            return data;
        }

        public KeyValuePair<int, string> GetCareerLevel(int level) {
            return GetAllCareerLevels().Where(e => e.Key == level).FirstOrDefault();
        }

        public Mission GetMission(int missionid) {
            var repo = Repository<Mission>.GetInstance();
            var data = repo.GetById(missionid);
            return data;
        }

        public ObjectiveGrade GetObjetiveTypeDetail(int typeid, int gradeid) {
            var repo = Repository<ObjectiveGrade>.GetInstance();
            var data = repo.FindBy(e => e.ObjectiveTypeId == typeid).Where(e => e.Grade == gradeid).FirstOrDefault();
            return data;
        }

        public Quality GetQuality(int qualityid) {
            var repo = Repository<Quality>.GetInstance();
            var data = repo.GetById(qualityid);

            return data;
        }

        public Scene GetScene(int sceneid) {
            var repo = Repository<Scene>.GetInstance();
            var data = repo.GetById(sceneid);
            return data;
        }

        public SceneObjective GetSceneObjective(int objectiveid) {
            var repo = Repository<SceneObjective>.GetInstance();
            var data = repo.GetById(objectiveid);
            return data;
        }

        public void RemoveMissionQuality(int missionid, int qualityid) {
            var repo = Repository<MissionQuality>.GetInstance();
            var data = repo.FindBy(e => e.MissionId == missionid).Where(e => e.QualityId == qualityid).FirstOrDefault();
            if (data != null) {
                repo.Remove(data.Id);
                repo.Commit();
            }
        }

        public Campaign SaveCampaign(Campaign entity) {
            var repo1 = Repository<Campaign>.GetInstance();

            if (entity.Id > 0)
                entity = repo1.Edit(entity);
            else
                entity = repo1.Create(entity);

            repo1.Commit();

            return entity;
        }

        public Mission SaveMission(Mission entity) {
            var repo = Repository<Mission>.GetInstance();
            if (entity.Id > 0)
                entity = repo.Edit(entity);
            else
                entity = repo.Create(entity);

            repo.Commit();

            return entity;
        }

        public Scene SaveScene(Scene entity) {
            var repo = Repository<Scene>.GetInstance();
            if (entity.ID > 0) {
                entity = repo.Edit(entity);
            }
            else {
                entity.CreatedOn = DateTime.Now;
                entity = repo.Create(entity);
            }
            repo.Commit();

            return entity;
        }

        public SceneObjective SaveSceneObjective(SceneObjective entity) {
            var repo = Repository<SceneObjective>.GetInstance();
            var rep1 = Repository<ObjectiveGrade>.GetInstance();

            var grade = rep1.FindBy(e => e.ObjectiveTypeId == entity.ObjectiveTypeID).Where(e => e.Grade == entity.GradeId).FirstOrDefault();
            entity.GradeId = grade.Id;

            if (entity.Id > 0) {
                entity = repo.Edit(entity);
            }
            else {
                var order = repo.FindBy(e => e.SceneId == entity.SceneId).Max(e => e.Order) + 1;
                entity.Order = order;
                entity = repo.Create(entity);
            }
            repo.Commit();

            return entity;
        }

        public SceneObjective SaveSceneObjectiveOrder(int sceneid, int objectiveid, int offset) {
            var repo = Repository<SceneObjective>.GetInstance();

            var source = repo.GetById(objectiveid);
            var newOrder = (source.Order + offset) == 0 ? 1 : (source.Order + offset);

            var target = repo.FindBy(e => e.SceneId == sceneid).Where(e => e.Order == newOrder).FirstOrDefault();
            var maxOrder = repo.FindBy(e => e.SceneId == sceneid).Max(e => e.Order);

            if (target != null) {
                target.Order = source.Order;
            }

            source.Order = (newOrder > maxOrder ? maxOrder : newOrder);

            repo.Commit();

            return source;
        }
    }
}