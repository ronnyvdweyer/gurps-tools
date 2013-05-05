using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCv20_Tools.Core;
using SCv20_Tools.Core.Data;
using SCv20_Tools.Core.Services;
using SCv20_Tools.Web.Framework;

namespace SCv20_Tools.Web.Areas.Api.Controllers {
    public class CampaignController : Controller {
        private readonly IRepository<Campaign> _campaignRepository;
        private readonly IRepository<Quality> _qualityRepository;

        public static readonly object x = new object();

        public CampaignController() {
            _campaignRepository = Repository<Campaign>.GetInstance();
            _qualityRepository = Repository<Quality>.GetInstance();

        }

        [HttpPost, AjaxHandleError]
        public JsonResult AddQuality(int campaignId, int qualityId) {
            lock (x) {
                var c = _campaignRepository.FindBy(e => e.Id == campaignId).FirstOrDefault();
                var q = _qualityRepository.FindBy(e => e.Id == qualityId).FirstOrDefault();

                c.Qualities.Add(q);
                _campaignRepository.Commit();

                return Json(new { done = true });
            }
        }
    }
}
