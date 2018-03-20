using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cBaseQMS.Areas.cBaseQMS.Helpers
{
    public class CustomModelBinder : DefaultModelBinder
    {
        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            controllerContext.Controller.ViewData.Model = bindingContext.Model;
            base.OnModelUpdated(controllerContext, bindingContext);
        }
    }
}