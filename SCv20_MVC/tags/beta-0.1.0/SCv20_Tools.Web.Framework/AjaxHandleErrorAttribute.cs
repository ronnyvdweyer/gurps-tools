using System;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace SCv20_Tools.Web.Framework {

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AjaxHandleErrorAttribute : HandleErrorAttribute {

        public override void OnException(ExceptionContext filterContext) {
            if (filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.Exception != null) {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                if (filterContext.Exception.GetType() == typeof(InvalidModelStateException)) {
                    var modelState = filterContext.Controller.ViewData.ModelState;
                    var modelErrors = new Dictionary<string, string>();

                    foreach (var key in modelState.Keys) {
                        if (modelState[key].Errors.Count > 0) {
                            for (int i = 0; i < modelState[key].Errors.Count; i++) {
                                modelErrors[key] = modelState[key].Errors[i].ErrorMessage;
                            }
                        }
                    }

                    //http://stackoverflow.com/questions/7287412/jquery-validate-asp-net-mvc-modelstate-errors-async-post
                    var json = new {
                        isvalidation = true,
                        message = "There are some validation errors in your data.",
                        stack   = string.Empty, //TODO: May be I'll change it to show more errors
                        errors  = modelErrors.ToArray()
                    };

                    filterContext.Result = new JsonNetResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = json };
                }
                else {
                    var json = new {
                        isvalidation = false,
                        message = filterContext.Exception.Message,
                        stack   = filterContext.Exception.ToString(),
                        errors  = new Dictionary<string, string>().ToArray()
                    };

                    filterContext.Result = new JsonNetResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = json };
                }

                filterContext.ExceptionHandled = true;
            }
            else {
                base.OnException(filterContext);
            }
        }
    }
}