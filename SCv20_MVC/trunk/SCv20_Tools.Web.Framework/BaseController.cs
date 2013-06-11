using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SCv20_Tools.Web.Framework {
    public class BaseController : System.Web.Mvc.Controller {
        private class ModelError {
            private string _field;
            private string _error;
            public ModelError(string field, string error) { 
                _field = field; _error = error; 
            }
            public string Field { get { return _field; } }
            public string Error { get { return _error; } }
        }


        /// <summary>
        /// Creates a <see cref="T:System.Web.Mvc.JsonResult" /> object that serializes the specified object to JavaScript Object 
        /// Notation (JSON) format using the content type, content encoding, and the JSON request behavior.
        /// </summary>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <param name="behavior">The JSON request behavior</param>
        /// <returns>
        /// The result object that serializes the specified object to JSON format.
        /// </returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) {

            var json = new JsonNetResult {
                Data                = data,
                ContentType         = contentType,
                ContentEncoding     = contentEncoding,
                JsonRequestBehavior = behavior
            };

            return json;
        }


        protected JsonResult AjaxResult(object model) {
            List<ModelError> errorList = null;

            if (!ModelState.IsValid) {
                errorList = new List<ModelError>();

                foreach (var key in ModelState.Keys) {
                    //if (ModelState[key].Errors.Count > 0) {
                    for (int i = 0; i < ModelState[key].Errors.Count; i++) {
                        errorList.Add(new ModelError(key, ModelState[key].Errors[i].ErrorMessage));
                    }
                    //}
                }
            }

            var ajaxModel = new {
                valid = ModelState.IsValid,
                errors = errorList,
                message = string.Empty,
                model = model
            };

            return Json(ajaxModel, JsonRequestBehavior.AllowGet);
        }
    }
}
