using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCv20_Tools.Core.Domain;
using SCv20_Tools.Core.Services;
using SCv20_Tools.Web.Framework;
using SCv20_Tools.Web.Models;

namespace SCv20_Tools.Web.Controllers {
    public class MissionController : Controller {
        private readonly DataService _dataService;

        public MissionController() {
            _dataService = DataService.GetInstance();
        }


        [HttpGet]
        public ActionResult Index() {
            return View();
        }


        [HttpGet]
        public ActionResult Listing() {
            var model = new MissionListingModel();
            var list = _dataService.GetAllMissions();

            var cals = _dataService.GetAllCalibers();

            model.Missions = list.Select(item => new MissionSummaryModel {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code,
                CaliberFormatted = item.Caliber.Value,
                Motivation = item.Motivation,
                AdjustedThreatLevel = item.AdjustedThreatLevel,
                Briefing = item.Briefing,
                CaliberList = cals.Select(cal => CaliberModel.MapFrom(cal)).ToList()
            }).ToList();



            return View(model);
        }


        [HttpGet]
        public ActionResult Editing(int id) {
            var model = new MissionSummaryModel();

            var mission = _dataService.GetMission(id);
            PrepareModelForView(model, mission);

            return View(model);
        }


        [HttpPost, FormValueRequired("save-edit")]
        public ActionResult Editing(MissionSummaryModel model) {
            var mission = _dataService.SaveMission(model.ToEntity());
            PrepareModelForView(model, mission);
            return View(model);
        }


        [HttpGet]
        public ActionResult AddQuality(int? missionid, int? id) {
            return Json(new {
                mission_id = missionid,
                quality_id = id
            }, JsonRequestBehavior.AllowGet);
        }


        [NonAction]
        private void PrepareModelForView(MissionSummaryModel model, Mission mission) {
            if (model == null)
                throw new ArgumentException("model");

            var calibers = _dataService.GetAllCalibers();
            model.CaliberList = calibers.Select(item => CaliberModel.MapFrom(item)).ToList();

            if (mission != null) {
                model.MapFrom(mission);
            }
        }
    }
}
