using System.Web.Mvc;

namespace cBaseCore.Areas.cBaseQMS
{
    public class cBaseQMSAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "cBaseQMS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "cBaseQMS_default",
                "cBaseQMS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}