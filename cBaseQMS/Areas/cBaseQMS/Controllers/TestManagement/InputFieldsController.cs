using cBaseQMS.Areas.cBaseQMS.Helpers;
using cBaseQMS.Areas.cBaseQMS.RestController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cBaseCore.Areas.cBaseQMS.Controllers.TestManagement
{
    public class InputFieldsController : BaseController
    {
        private readonly ITestClientService _iTestClient;
        private readonly ITestMasterClient _iTestMaserClient;
        public InputFieldsController(ITestMasterClient _iTestMaserClient, ITestClientService _iTestClient) 
        {
            this._iTestMaserClient = _iTestMaserClient;
            this._iTestClient = _iTestClient;
            this._iTestClient.ModelState = this.ModelState;
            this._iTestMaserClient.ModelState = this.ModelState;
        }
        // GET: cBaseQMS/InputFields
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllTestMasters()
        {
            var objGetTestMasterByTestIdViewModel = _iTestMaserClient.GetAll();

            var jsonData = new { rows = objGetTestMasterByTestIdViewModel };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}