using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCv20_Tools.Core.Services;
using SCv20_Tools.Web.Framework;
using SCv20_Tools.Web.Models;

namespace SCv20_Tools.Web.Controllers {
    public class DummyModel {
        public string Mensagem {
            get;
            set;
        }
    }


    public class CampaignsController : BaseController {
        private readonly DataService _dataService;
        public static readonly object x = new object();

        public CampaignsController() {
            _dataService = DataService.GetInstance();
        }


        public ActionResult Index() {
            return View(new DummyModel { Mensagem = "Apenas um Teste do Controller" });
        }


        [HttpGet]
        public ActionResult Create() {

            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id) {
            lock (x) {
                if (id <= 0)
                    return RedirectToAction("Create");

                var entity = _dataService.GetCampaign(id);

                var model = CampaignModel.MapFrom(entity);
                PrepareModelForView(model);

                return View(model);
            }
        }


        [HttpPost, FormValueRequired("save")]
        public ActionResult Edit(dynamic model) {

            return View();
        }


        [HttpGet, AjaxHandleError]
        public ActionResult GetQualityDetails(int id) {
            lock (x) {
                var data = _dataService.GetQuality(id);
                var model = QualityModel.MapFrom(data);

                return Json(new {
                    bonusAD = model.BonusADFormated,
                    bonusXP = model.BonusXPFormated,
                    description = model.Description
                }, JsonRequestBehavior.AllowGet);
            }
        }

        private CampaignModel PrepareModelForView(CampaignModel model) {
            lock (x) {
                if (model == null)
                    model = new CampaignModel();

                var qualities = _dataService.GetAllQualitiesExcludingExistingFromCampaign(model.Id);
                model.ListAvaliableQualities = qualities.Select(item => QualityModel.MapFrom(item)).ToList();

                var conversions = _dataService.GetAllHistoricalConversions();
                model.ListHistoricalConversions = conversions.Select(item => HistoricalConversionModel.MapFrom(item)).ToList();

                return model;
            }
        }
    }
}
