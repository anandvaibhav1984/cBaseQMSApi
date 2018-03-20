
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cBaseQMS.Models;



using Elmah;
using cBaseQMS.Areas.cBaseQMS.Common;
using cBaseQMS.Areas.cBaseQMS.Models;

namespace cBaseCore.Areas.cBaseQMS.Controllers.Settings
{
    public class ApplicationSettingController : Controller
    {
        //private readonly IAppParameterService appParameterService;
        //private readonly IUnitOfWork unitOfWork;
        //public ApplicationSettingController(IAppParameterService appParameterService, IUnitOfWork unitOfWork)
        //{
        //    this.appParameterService = appParameterService;
        //    this.unitOfWork = unitOfWork;
        //}
        //// GET: cBaseQMS/ApplicationSetting
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public JsonResult AppParameterData(string sort, int page, int rows, bool status)
        //{
        //    sort = (sort == null) ? "" : sort;
        //    int pageIndex = Convert.ToInt32(page) - 1;
        //    int pageSize = rows;

           
        //    var data = this.appParameterService.GetParameters();
        //    if (status)
        //    {
        //        data = data.Where(x => x.IsActive != false).ToList();
        //    }
        //    int totalRecords = data.Count();

        //    var totalPages = (int)Math.Ceiling((float)totalRecords / rows);

        //    var jsonData = new
        //    {
        //        total = totalPages,
        //        page,
        //        records = totalRecords,
        //        rows = data
        //    };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult AddAppParam(AppParamViewModel appParamModel)
        //{
        //    appParamModel.IsActive = true;
        //    AppParameter objAppParam = null;
        //    objAppParam = Mapper.Map<AppParamViewModel, AppParameter>(appParamModel);
        //    this.appParameterService.SaveAppParameters(objAppParam);
        //    unitOfWork.Commit();
            
        //    return this.Content("Success");
        //}
    }
}