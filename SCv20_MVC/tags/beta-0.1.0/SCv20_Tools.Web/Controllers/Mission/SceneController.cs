using System.Linq;
using System.Web.Mvc;
using SCv20_Tools.Core.Services;
using SCv20_Tools.Web.Framework;
using SCv20_Tools.Web.Models;

namespace SCv20_Tools.Web.Controllers {

    public class SceneController : BaseController {
        private readonly DataService _dataService;

        public SceneController() {
            _dataService = DataService.GetInstance();
        }


        [HttpGet]
        public ActionResult Create(int missionid) {
            var exist = _dataService.GetMission(missionid);
            if (exist == null)
                return RedirectToAction("Create", "Mission");

            var model = SceneModel.CreateFrom(null);
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(SceneModel model) {
            var entity = model.MapToSceneEntity();

            if (!ModelState.IsValid) {
                return View(model);
            }

            _dataService.SaveScene(entity);

            return View(model);
        }


        [HttpGet] // GET: Mission/{missionid}/Scenes/Editing/{id}
        public ActionResult Editing(int missionid, int id) {
            var data = _dataService.GetScene(id);

            if (data == null)
                return RedirectToAction("Create", new { missionid = missionid });

            var model = SceneModel.CreateFrom(data);

            return View(model);
        }


        [HttpPost] // POST: Mission/{missionid}/Scenes/Editing/{id}
        public ActionResult Editing(SceneModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            var scene = _dataService.SaveScene(model.MapToSceneEntity());

            return RedirectToAction("Editing", new { missionid = scene.MissionID, id = scene.ID });
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post), AjaxHandleError]   //POST/GET: Mission/{missionid}/Scenes/Objective/{id}
        public ActionResult GetObjetive(int id) {
            var objective = _dataService.GetSceneObjective(id);
            var calibers = _dataService.GetAllCalibers();
            var types = _dataService.GetAllObjectiveTypes();

            var model = SceneObjectiveModel.CreateFrom(objective, calibers, types);

            return PartialView("_sceneObjective", model);
        }

        
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post), AjaxHandleError]
        public ActionResult GetObjetiveList(int sceneid) {
            var model = _dataService.GetAllSceneObjectives(sceneid).Select(item => item.Id).ToList();
            return PartialView("Listing/_sceneObjectiveList", model);
        }


        [HttpGet, AjaxHandleError]
        public ActionResult CreateEmptyObjective(int sceneid) {
            var calibers = _dataService.GetAllCalibers();
            var types = _dataService.GetAllObjectiveTypes();

            var model = SceneObjectiveModel.CreateFrom(null, calibers, types);
            model.SceneID = sceneid;

            return PartialView("_sceneObjective", model);
        }


        [HttpPost, AjaxHandleError] // POST: /Scene/SaveObjective/{id}
        public ActionResult SaveObjetive(SceneObjectiveModel model) {
            if (!ModelState.IsValid)
                return AjaxResult(model);

            var entity = model.MapToSceneObjectiveEntity();
            entity = _dataService.SaveSceneObjective(entity);

            return AjaxResult(new { id = entity.Id });
        }


        [HttpPost, AjaxHandleError]
        public ActionResult ReorderObjective(int sceneid, int objectiveid, int offset) {
            var entity = _dataService.SaveSceneObjectiveOrder(sceneid, objectiveid, offset);
            var model = _dataService.GetAllSceneObjectives(sceneid).Select(item => item.Id).ToList();

            return PartialView("Listing/_sceneObjectiveList", model);
        }
    }
}