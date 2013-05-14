using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCv20_Tools.Core;
using SCv20_Tools.Core.Data;
using SCv20_Tools.Core.Domain;
using SCv20_Tools.Core.Services;
using SCv20_Tools.Web.Areas.Api.Models;
using SCv20_Tools.Web.Framework;

namespace SCv20_Tools.Web.Areas.Api.Controllers {
    public class CampaignController : Controller {
        private readonly IRepository<Campaign> _campaignRepository;
        private readonly IRepository<CampaignQuality> _campaignQualityRepository;
        private readonly IRepository<Quality> _qualityRepository;
        private readonly DataService _dataService;


        public CampaignController() {
            _campaignRepository = Repository<Campaign>.GetInstance();
            _campaignQualityRepository = Repository<CampaignQuality>.GetInstance();
            _qualityRepository = Repository<Quality>.GetInstance();
            _dataService = DataService.GetInstance();

        }


        [HttpPost, AjaxHandleError]
        public JsonResult AddQuality(int campaignId, int qualityId) {
            var c = _campaignRepository.FindBy(e => e.Id == campaignId).FirstOrDefault();
            var q = new CampaignQuality { CampaignId = campaignId, QualityId = qualityId };

            c.Qualities.Add(q);
            _campaignRepository.Commit();

            return Json(new { done = true });
        }


        [HttpGet, AjaxHandleError]
        public JsonResult GetAvaliableQualities(int campaignId) {
            var q = _dataService.GetAllQualitiesExcludingExistingFromCampaign(campaignId);

            var list = q.Select(item => QualityModel.MapFrom(item));

            return Json(new { done = true, ds = list }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet, AjaxHandleError]
        public JsonResult GetQualities(int campaignId) {
            var c = _campaignRepository.FindBy(e => e.Id == campaignId).FirstOrDefault();

            if (c != null) {
                var list = c.Qualities.Select(item => QualityModel.MapFrom(item.Quality)).OrderBy(e => e.name).ToList();
                var json = new { done = true, ds = list };
                return Json(json, JsonRequestBehavior.AllowGet);
            }

            return Json(new { done = false });
        }


        [HttpPost, AjaxHandleError]
        public JsonResult RemoveQuality(int campaignId, int qualityId) {
            if (campaignId == 0) {
                ModelState.AddModelError("YearDetails", "Campo é obrigatorio");
                ModelState.AddModelError("BaseNetWorthFormatted", "Campo é obrigatorio");
                throw new InvalidModelStateException(campaignId);
            }

            var q = _campaignQualityRepository
                .FindBy(e => e.CampaignId == campaignId)
                .Where(e => e.QualityId == qualityId)
                .FirstOrDefault();

            if (q == null)
                return Json(new { done = false, ds = "Not found" });

            _campaignQualityRepository.Remove(q.Id);
            _campaignQualityRepository.Commit();

            return Json(new { done = true });
        }


        [HttpPost, AjaxHandleError]
        public JsonResult Save(CampaignModel form) {
            if (!ModelState.IsValid) {
                throw new InvalidModelStateException(form);
            }
            
            var c = _campaignRepository.FindBy(e => e.Id == form.id).FirstOrDefault();

            if (c == null)
                c = new Campaign();

            c.Name = form.name;
            c.StartingLevel = form.startitngLevel;
            c.Summary = form.summary;
            c.YearDetails = form.yearDetails;
            c.YearId = form.yearId;
            _campaignRepository.Edit(c);
            _campaignRepository.Commit();

            return null;
        }

    }
}
