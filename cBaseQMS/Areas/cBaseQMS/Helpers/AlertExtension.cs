using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cBaseQMS.Areas.cBaseQMS.Helpers
{
    public static class AlertExtension
    {
        private static readonly string _alertKey = "_Alert";

        public static List<Alert> GetAlerts(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(_alertKey))
            {
                tempData[_alertKey] = new List<Alert>();
            }

            return (List<Alert>)tempData[_alertKey];
        }

        public static ActionResult WithSuccess(this ActionResult actionResult, List<string> message)
        {
            return new AlertDecoratorResult(actionResult, "alert-info", message);
        }

        public static ActionResult WithInfo(this ActionResult actionResult, List<string> message)
        {
            return new AlertDecoratorResult(actionResult, "alert-info", message);
        }

        public static ActionResult WithWarning(this ActionResult actionResult, List<string> message)
        {
            return new AlertDecoratorResult(actionResult, "alert-warning", message);
        }

        public static ActionResult WithError(this ActionResult actionResult, List<string> message)
        {
            return new AlertDecoratorResult(actionResult, "alert-danger", message);
        }

        public static ActionResult WithValidationError(this ActionResult actionResult, List<string> message)
        {
            return new AlertDecoratorResult(actionResult, "alert-danger", message);
        }
    }
}