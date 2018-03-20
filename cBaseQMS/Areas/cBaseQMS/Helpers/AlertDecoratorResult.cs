using FluentValidation;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace cBaseQMS.Areas.cBaseQMS.Helpers
{
    public class AlertDecoratorResult : ActionResult
    {
        private readonly ActionResult _innerResult;
        private readonly string _alertClass;
        private readonly List<string> _message;

        public AlertDecoratorResult(ActionResult innerResult, string alertClass, List<string> message)
        {
            _innerResult = innerResult;
            _alertClass = alertClass;
            _message = message;
        }


        public override void ExecuteResult(ControllerContext context)
        {
            var alerts = context.Controller.TempData.GetAlerts();
            alerts.Add(new Alert(_alertClass, _message));
            _innerResult.ExecuteResult(context);
        }
    }
}