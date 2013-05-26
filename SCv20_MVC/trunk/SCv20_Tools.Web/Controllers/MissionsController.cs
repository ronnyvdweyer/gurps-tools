using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCv20_Tools.Core.Services;
using SCv20_Tools.Web.Models;

namespace SCv20_Tools.Web.Controllers {
    public class MissionsController : Controller {
        private readonly DataService _dataService;

        public MissionsController() {
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
                MissionId           = item.Id,
                Name                = item.Name,
                Code                = item.Code,
                Caliber             = item.Caliber.Value,
                Motivation          = item.Motivation,
                AdjustedThreatLevel = item.AdjustedThreatLevel,
                Briefing            = item.Briefing,
                CaliberList         = cals.Select(cal => CaliberModel.MapFrom(cal)).ToList()
            }).ToList();



            return View(model);
        }
    }
}
