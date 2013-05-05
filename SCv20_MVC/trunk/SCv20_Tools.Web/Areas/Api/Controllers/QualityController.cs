using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCv20_Tools.Core;
using SCv20_Tools.Core.Data;
using SCv20_Tools.Web.Areas.Api.Models;
using SCv20_Tools.Web.Framework;

namespace SCv20_Tools.Web.Areas.Api.Controllers {

    public class QualityController : Controller {
        public static readonly object x = new object();
    
        private readonly IRepository<Quality> _qualityRepository;

        public QualityController() {
            _qualityRepository = Repository<Quality>.GetInstance();
        }


        [HttpGet, AjaxHandleError]
        public JsonResult GetDetails(int id) {
            lock (x) {

                var data = _qualityRepository.FindBy(q => q.Id == id).FirstOrDefault();
                var model = QualityModel.MapFrom(data);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet, AjaxHandleError]
        public JsonResult GetAll() {
            lock (x) {
                var data = _qualityRepository.FindAll().ToList();
                var model = data.Select(e => QualityModel.MapFrom(e));
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet, AjaxHandleError]
        public JsonResult GetSeasonsOnly() {
            lock (x) {
                var data = _qualityRepository.FindBy(q => q.IsSeasonsOnly == true).ToList();
                var model = data.Select(e => QualityModel.MapFrom(e));
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet, AjaxHandleError]
        public JsonResult GetUniversalOnly() {
            lock (x) {
                var data = _qualityRepository.FindBy(q => q.IsSeasonsOnly == false).ToList();
                var model = data.Select(e => QualityModel.MapFrom(e));
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
