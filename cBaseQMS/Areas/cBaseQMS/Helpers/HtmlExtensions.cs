using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace cBaseQMS.Areas.cBaseQMS.Helpers
{
    public static class HtmlExtensions
    {
        public static string BuildBreadcrumbNavigation(this HtmlHelper helper)
        {
            string area = (helper.ViewContext.RouteData.DataTokens["area"] ?? "").ToString();
            string controller = helper.ViewContext.RouteData.Values["controller"].ToString();
            string action = helper.ViewContext.RouteData.Values["action"].ToString();

            // add link to homepage by default
            StringBuilder breadcrumb = new StringBuilder(@"
                <ol class='breadcrumb'>
                    <li>" + helper.ActionLink("Homepage", "Index", "Home", new { Area = "" }, new { @class = "first" }) + @"</li>");

            // add link to area if needed
            //if (area != "")
            //{
            //    breadcrumb.Append("<li>");
            //    if (ControllerExistsInArea("Default", area)) // by convention, default Area controller should be named Default
            //    {
            //        breadcrumb.Append(helper.ActionLink(area.AddSpaceOnCaseChange(), "Index", "Default", new { Area = area }, new { @class = "" }));
            //    }
            //    else
            //    {
            //        breadcrumb.Append(area.AddSpaceOnCaseChange());
            //    }
            //    breadcrumb.Append("</li>");
            //}

            // add link to controller Index if different action
            if ((controller != "Home" && controller != "Default") && action != "Index")
            {
                if (ActionExistsInController("Index", controller, area))
                {
                    breadcrumb.Append("<li>");
                    breadcrumb.Append(helper.ActionLink(controller.AddSpaceOnCaseChange(), "Index", controller, new { Area = area }, new { @class = "" }));
                    breadcrumb.Append("</li>");
                }
            }

            // add link to action
            if ((controller != "Home" && controller != "Default") || action != "Index")
            {
                breadcrumb.Append("<li>");
                breadcrumb.Append(helper.ActionLink((action.ToLower() == "index") ? controller.AddSpaceOnCaseChange() : action.AddSpaceOnCaseChange(), action, controller, new { Area = area }, new { @class = "" }));
                // breadcrumb.Append((action.ToLower() == "index") ? controller.AddSpaceOnCaseChange() : action.AddSpaceOnCaseChange());
                breadcrumb.Append("</li>");
            }

            return MvcHtmlString.Create(breadcrumb.Append("</ol>").ToString()).ToString();
        }

        public static Type GetControllerType(string controller, string area)
        {
            string currentAssembly = Assembly.GetExecutingAssembly().GetName().Name;
            IEnumerable<Type> controllerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(o => typeof(IController).IsAssignableFrom(o));

            string typeFullName = String.Format("{0}.Controllers.{1}Controller", currentAssembly, controller);
            if (area != "")
            {
                typeFullName = String.Format("{0}.Areas.{1}.Controllers.{2}Controller", currentAssembly, area, controller);
            }

            return controllerTypes.Where(o => o.FullName == typeFullName).FirstOrDefault();
        }

        public static bool ActionExistsInController(string action, string controller, string area)
        {
            Type controllerType = GetControllerType(controller, area);
            return (controllerType != null && new ReflectedControllerDescriptor(controllerType).GetCanonicalActions().Any(x => x.ActionName == action));
        }

        public static bool ControllerExistsInArea(string controller, string area)
        {
            Type controllerType = GetControllerType(controller, area);
            return (controllerType != null);
        }

        public static string AddSpaceOnCaseChange(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}