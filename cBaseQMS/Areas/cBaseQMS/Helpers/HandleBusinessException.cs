using Elmah;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace cBaseQMS.Areas.cBaseQMS.Helpers
{
   
        public class HandleBusinessException : HandleErrorAttribute
        {
            public HandleBusinessException()
            {
                ActionToRedirectIfInvalid = "Error";
                ForAjaxRequest = true;
            }

            public string ControllerToRedirectIfInvalid { get; set; }
            public string ActionToRedirectIfInvalid { get; set; }
            public bool ForAjaxRequest { get; set; }

            public override void OnException(ExceptionContext filterContext)
            {
                if (filterContext.Exception is FluentValidation.ValidationException)
                {
                    var validationException = filterContext.Exception as FluentValidation.ValidationException;
                    if (ForAjaxRequest)
                    {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new
                        {
                            Success = false,
                            ErrorCode = validationException.Errors.Select(e => e.ErrorCode),
                            msg = validationException.Errors.Select(e => e.ErrorMessage)
                            }
                        };
                    }
                    else
                    {
                        var controllerToRedirect = string.IsNullOrWhiteSpace(ControllerToRedirectIfInvalid)
                        ? filterContext.RouteData.Values["controller"].ToString()
                        : ControllerToRedirectIfInvalid;
                    var Area = filterContext.RouteData.DataTokens["area"];
                    List<string> error = new List<string>();
                    error = validationException.Errors.Select(e => e.ErrorMessage).ToList();
                    filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary(
                new
                {
                    action = ActionToRedirectIfInvalid,
                    Controller = controllerToRedirect
                    ,
                    area = Area
                })).WithError(error);
                }

                    filterContext.ExceptionHandled = true;
                }
                else
                {
                    var formParameters = filterContext.RequestContext.HttpContext.Request.Form;
                    var queryStringParameters = filterContext.RequestContext.HttpContext.Request.QueryString;
                    var routeParameters = filterContext.RouteData.Values;

                    var parameters = new List<string>();
                    parameters.AddRange(formParameters.Keys
                                                .Cast<string>()
                                                .Select(k => FormatParameterForLogging(k, formParameters[k])));
                    parameters.AddRange(queryStringParameters.Keys
                                                .Cast<string>()
                                                .Select(k => FormatParameterForLogging(k, queryStringParameters[k])));
                    parameters.AddRange(routeParameters.Select(p => FormatParameterForLogging(p.Key, p.Value)));
                //if needed Logging of exceptions and parameters

                filterContext.Result = new ViewResult
                {
                    ViewName = @"~/Areas/cBaseQMS/Views/Shared/Error.cshtml"
                };

                //    filterContext.Result = new RedirectToRouteResult(
                //new RouteValueDictionary(
                //    new
                //    {
                //        action = ActionToRedirectIfInvalid,
                //        Controller = "TestMaster",
                //        area = Area
                //    })).WithError(string.Join("<br/>", filterContext.Exception.ToString()));

                filterContext.ExceptionHandled = true;
                base.OnException(filterContext);
                }
            }

            private string FormatParameterForLogging(string key, object value)
            {
                return string.Format("[{0}: {1}]", key, JsonConvert.SerializeObject(value));
            }
        }
   
}