using System.Linq;
using System.Web.Mvc;

namespace cBaseQMS.Areas.cBaseQMS.Helpers
{

    public class HandleActionFilterAttribute : ActionFilterAttribute
    {
    //    public bool ForAjaxRequest { get; set; }
    //    public string Operation { get; set; }
        public string SuccessMsg{ get; set; }

    //    public HandleActionFilterAttribute()
    //    {
    //        ForAjaxRequest = true;

    //    }
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        ForViewModel = filterContext.ActionParameters.Select(p => p.Key.ToString()).FirstOrDefault();

    //        base.OnActionExecuting(filterContext);
    //    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var model = filterContext.Controller.ViewData.ModelState;
                //send model to json if want to get its value on client side
                var objectmodel = filterContext.Controller.ViewData.Model;
                string controller = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
                filterContext.Result = new JsonResult
                {
                   Data=new
                   {
                       Success = !model.IsValid ? false : true,
                       Code = !model.IsValid ? 500 : 200,
                       Message = model.IsValid ? controller.AddSpaceOnCaseChange() + "Created Successfuly":"",
                       error = model.Where(s => s.Value.Errors.Count > 0).Select(s => s.Value.Errors[0].ErrorMessage).ToList()
                   }
                };
               // return JsonResult(Data, JsonRequestBehavior.AllowGet);

                //filterContext.Result = new JsonResult
                //    {
                //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                //     };

            }
            base.OnActionExecuted(filterContext);
        }

    //    public override void OnResultExecuting(ResultExecutingContext filterContext)
    //    {
    //    }

    //    public override void OnResultExecuted(ResultExecutedContext filterContext)
    //    {
    //    }
    }
}