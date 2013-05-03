using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace SCv20_Tools.Web.Framework {
    public class AjaxHandleErrorAttribute : HandleErrorAttribute {


        public override void OnException(ExceptionContext filterContext) {


            if (filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.Exception != null) {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var tmp = new JsonResult {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new {
                        message = filterContext.Exception.Message,
                        stack   = filterContext.Exception.StackTrace
                    }
                };

                filterContext.Result = tmp;

                filterContext.ExceptionHandled = true;
            }
            else {
                base.OnException(filterContext);
            }
        }
    }
}
