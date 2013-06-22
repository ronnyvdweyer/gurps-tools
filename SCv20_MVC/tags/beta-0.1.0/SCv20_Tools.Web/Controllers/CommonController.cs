using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCv20_Tools.Core.Services;
using SCv20_Tools.Web.Framework;
using SCv20_Tools.Web.Models;

namespace SCv20_Tools.Web.Controllers {
    public class CommonController : BaseController {
        private readonly DataService _dataService;

        public CommonController() {
            _dataService = DataService.GetInstance();
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post), AjaxHandleError]
        public ActionResult GetObjectiveTypeDetail(int typeid, int gradeid) {
            var data = _dataService.GetObjetiveTypeDetail(typeid, gradeid);
            return Json(new { done = true, ds = ObjectiveModel.CreateFrom(data) }, JsonRequestBehavior.AllowGet);
        }
    }
}
