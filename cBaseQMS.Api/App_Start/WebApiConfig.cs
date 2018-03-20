﻿using cBaseQMS_Api.Filters;
using cBaseQMS_Api.Helpers;
using Elmah.Contrib.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace cBaseQMS.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ValidateModelStateFilter());
            config.MessageHandlers.Add(new MessageHandlerAttribute());
            // enable elmah
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                // constraint required so this route only matches valid controller names
                ,constraints: new { controller = GetControllerNames() }
                
            );

            // catch all route mapped to ErrorController so 404 errors
            // can be logged in elmah
            //add logic to ignore first time call as there will be no apicontroller to be called in our aplication
            //config.Routes.MapHttpRoute(
            //    name: "NotFound",
            //    routeTemplate: "{*path}",
            //    defaults: new { controller = "Error", action = "NotFound" }
            //);
        }

        // helper method that returns a string of all api controller names 
        // in this solution, to be used in route constraints above
        private static string GetControllerNames()
        {
            var controllerNames = Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(x =>
                    x.IsSubclassOf(typeof(ApiController)) &&
                    x.FullName.StartsWith(MethodBase.GetCurrentMethod().DeclaringType.Namespace + ".Controllers"))
                .ToList()
                .Select(x => x.Name.Replace("Controller", ""));

            return string.Join("|", controllerNames);
        }
    }
}
