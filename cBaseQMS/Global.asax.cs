
using cBaseQMS.App_Start;
using cBaseQMS.Areas.cBaseQMS.Helpers;

using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace cBaseQMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
         
          Bootstrapper.Run();
            ModelBinders.Binders.DefaultBinder = new CustomModelBinder();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        /* to filter specific log
        protected void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError404(e);
        }

      

        // Dismiss 404 errors for ELMAH
        private void FilterError404(ExceptionFilterEventArgs e)
        {
            if (e.Exception.GetBaseException() is HttpException)
            {
                HttpException ex = (HttpException)e.Exception.GetBaseException();
                if (ex.GetHttpCode() == 404) { }
                // e.Dismiss();
            }
        }
        */
    }
}
