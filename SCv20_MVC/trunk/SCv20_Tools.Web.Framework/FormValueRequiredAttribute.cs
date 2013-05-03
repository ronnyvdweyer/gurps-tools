using System;
using System.Diagnostics;
using System.Reflection;
using System.Web.Mvc;

namespace SCv20_Tools.Web.Framework {

    public enum FormValueRequirement {
        Equal,
        StartsWith
    }

    public class FormValueRequiredAttribute : ActionMethodSelectorAttribute {
        private readonly FormValueRequirement _requirement;
        private readonly string[] _submitButtonNames;
        /// <summary>
        /// Initializes a new instance of the <see cref="FormValueRequiredAttribute"/> class.
        /// </summary>
        /// <param name="submitButtonNames">The submit button names.</param>
        public FormValueRequiredAttribute(params string[] submitButtonNames) :
            this(FormValueRequirement.Equal, submitButtonNames) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormValueRequiredAttribute"/> class.
        /// </summary>
        /// <param name="requirement">The requirement.</param>
        /// <param name="submitButtonNames">The submit button names.</param>
        public FormValueRequiredAttribute(FormValueRequirement requirement, params string[] submitButtonNames) {
            //at least one submit button should be found
            this._submitButtonNames = submitButtonNames;
            this._requirement = requirement;
        }

        /// <summary>
        /// Determines whether the action method selection is valid for the specified controller context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="methodInfo">Information about the action method.</param>
        /// <returns>
        /// true if the action method selection is valid for the specified controller context; otherwise, false.
        /// </returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo) {
            foreach (string buttonName in _submitButtonNames) {
                try {
                    string value = "";
                    switch (this._requirement) {
                        case FormValueRequirement.Equal: {
                                //do not iterate because "Invalid request" exception can be thrown
                                value = controllerContext.HttpContext.Request.Form[buttonName];
                            }
                            break;

                        case FormValueRequirement.StartsWith: {
                                foreach (var formValue in controllerContext.HttpContext.Request.Form.AllKeys) {
                                    if (formValue.StartsWith(buttonName, StringComparison.InvariantCultureIgnoreCase)) {
                                        value = controllerContext.HttpContext.Request.Form[formValue];
                                        break;
                                    }
                                }
                            }
                            break;
                    }
                    if (!String.IsNullOrEmpty(value))
                        return true;
                }
                catch (Exception exc) {
                    //try-catch to ensure that
                    Debug.WriteLine(exc.Message);
                }
            }
            return false;
        }
    }
}