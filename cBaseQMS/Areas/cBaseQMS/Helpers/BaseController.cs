using cBaseQMS.Areas.cBaseQMS.RestController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace cBaseQMS.Areas.cBaseQMS.Helpers
{
    public class BaseController :Controller
    {
     
        protected RedirectResult RedirectToAction<T>(Expression<Action<T>> action, RouteValueDictionary values = null) where T : Controller
        {
            return new RedirectResult(RedirectionHelper.GetUrl(action, Request.RequestContext, values));
        }

        protected Dictionary<string,string> GetQueryStringValue(HttpContextBase httpContext)
        {
            var querystring = HttpContext.Request.UrlReferrer.Query;
            var collection = querystring.getUrlParam(this.HttpContext);
            return collection;
        }
    }
}