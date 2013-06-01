using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCv20_Tools.Core.Services;
using SCv20_Tools.Web.Models;

namespace SCv20_Tools.Web.Controllers {
    public class CommonController : Controller {
        private readonly DataService _dataService;

        public CommonController() {
            _dataService = DataService.GetInstance();
        }
    }
}
