using cBaseQMS.Areas.cBaseQMS.Helpers;
using cBaseQMS.Areas.cBaseQMS.Models;
using cBaseQMS.Areas.cBaseQMS.RestController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace cBaseCore.Areas.cBaseQMS.Controllers.TestManagement
{
    public class TestsController : BaseController
    {
        private readonly ITestClientService _iTestClient;
        private readonly ITestMasterClient _iTestMaserClient;
        public TestsController(ITestClientService _iTestClient, ITestMasterClient _iTestMaserClient) 
        {
            this._iTestMaserClient = _iTestMaserClient;
            this._iTestClient = _iTestClient;
            this._iTestClient.ModelState = this.ModelState;
            this._iTestMaserClient.ModelState = this.ModelState;
        }

        // GET: cBaseQMS/Tests
        public ActionResult Index(int? testMasterid)
        {
            ViewBag.testmasterid = testMasterid;
            return View(testMasterid);
        }

        [HttpGet]
        public JsonResult GetAllTestMasters(int? testMasterid,Boolean ForCopyTo=false)
        {
            IEnumerable<SelectListItem> testMasterList = new List<SelectListItem>();
            if (!ForCopyTo)
            {
                //use of helper method to create select list item
                testMasterList = SelectListItemAdapter.GetSelectListItemCollection(_iTestMaserClient.GetAll(), o => o.TestMasterName, o => o.TestMasterID.ToString(), Convert.ToString(testMasterid));               
            }
            else
            {
                testMasterList = SelectListItemAdapter.GetSelectListItemCollection(_iTestMaserClient.GetAll(), o => o.TestMasterName, o => Convert.ToString(o.TestMasterID));               
            }
            return Json(testMasterList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetAllTestByTestMasterID(int testMasterid, string active,bool redirect )
        {
            var testList = _iTestClient.GetAllTestByTestMasterID(testMasterid, active,redirect);
            var jsonData = new { rows = testList };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult CreateTest(TestsViewModel testModel)
        {
           
            var status = _iTestClient.CreateTest(testModel);
            //var querystring = HttpContext.Request.UrlReferrer.Query;
            //helper method to fetch all query parameter
            //var collection = querystring.getUrlParam(this.HttpContext);
            //collection["testMasterid"]

            var Data = new
            {
                Success = !ModelState.IsValid ? false : true,
                Code = !ModelState.IsValid ? 500 : 200,
                Message = !ModelState.IsValid ? "ERROR IN CREATING TEST " : "Test  Created Successfuly",
                testMasterId = GetQueryStringValue(this.HttpContext)["testMasterid"],
                error = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new { s.Key, s.Value.Errors[0].ErrorMessage }).ToList()
            };
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HandleBusinessException]
        public JsonResult ShiftSequence(int currentKeyID, int nextKeyID, int prevKeyID, string opCode, int testMasterId)
        {
            var result = _iTestClient.ShiftSequence(currentKeyID, nextKeyID, prevKeyID, opCode, testMasterId);

            var Data = new
            {
                Success = !ModelState.IsValid ? false : true,
                TestMasterId = testMasterId,
                Message = "Sequence Updated",
                Code = !ModelState.IsValid ? 500 : 200,
                error = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new { s.Key, s.Value.Errors[0].ErrorMessage }).ToList()
            };
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoToTestMasterPage()
        {
            return RedirectToAction<TestMasterController>(x => x.Index(string.Empty));
        }
        public JsonResult RemoveFromTestMaster(TestsViewModel testsViewModel)
        {
            var result = _iTestClient.RemoveFromTestMaster(testsViewModel);
            var Data = new
            {
                Success = !ModelState.IsValid ? false : true,
                TestMasterId = testsViewModel.TestMasterID,
                Message = "Test Removed",
                Code = !ModelState.IsValid ? 500 : 200,
                error = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new { s.Key, s.Value.Errors[0].ErrorMessage }).ToList()
            };
            return Json(Data, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UpdateTest(TestsViewModel testsViewModel)
        {
            var result = _iTestClient.UpdateTest(testsViewModel);
            
            var Data = new
            {
                Success = !ModelState.IsValid ? false : true,
                testMasterId = GetQueryStringValue(this.HttpContext)["testMasterid"],
                Code = !ModelState.IsValid ? 500 : 200,
                Message = (testsViewModel.Operation == "Delete") ? "Test  Removed" : "Test Updated!",
                error = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new { s.Key, s.Value.Errors[0].ErrorMessage }).ToList()
            };
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MoveTest(TestsViewModel testsViewModel,int TestMasterIdFrom)
        {
            var result = _iTestClient.MoveTest(testsViewModel,TestMasterIdFrom);
            var Data = new
            {
                Success = !ModelState.IsValid ? false : true,
                testMasterId = testsViewModel.TestMasterID,
                Code = !ModelState.IsValid ? 500 : 200,
                Message = (testsViewModel.Operation.ToLower() == "move") ? "Test  moved " : "",
                error = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new { s.Key, s.Value.Errors[0].ErrorMessage }).ToList()
            };
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

    }
}