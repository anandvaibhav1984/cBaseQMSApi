using cBaseQMS.Areas.cBaseQMS.Helpers;
using cBaseQMS.Areas.cBaseQMS.Models;
using cBaseQMS.Areas.cBaseQMS.RestController;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace cBaseCore.Areas.cBaseQMS.Controllers.TestManagement
{
    public class TestMasterController : BaseController
    {
        private readonly ITestClientService _iTestClient;
        private readonly ITestMasterClient _iTestMaserClient;
        public TestMasterController(ITestMasterClient _iTestMaserClient, ITestClientService _iTestClient)
        {
            this._iTestMaserClient = _iTestMaserClient;
            this._iTestClient = _iTestClient;
            this._iTestClient.ModelState = this.ModelState;
            this._iTestMaserClient.ModelState = this.ModelState;


        }

        /// <summary>
        ///    Get all part and location mapping for testmaster
        /// </summary>
        /// <param name="testMasterMappingViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        //[HandleBusinessException]
       // [HandleActionFilter]
        public ActionResult checkExistence(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            List<string> errorList = new List<string>();
            _iTestMaserClient.ifExistsLocationAndPartMapping(testMasterMappingViewModel);
            //if (!ModelState.IsValid)
            //{
            //    return PartialView("_Notification").WithValidationError(
            //        ModelState.Where(s => s.Value.Errors.Count > 0).SelectMany(s => new List<string> { s.Value.Errors[0].ErrorMessage }).ToList()
            //        );
            //}
            //else return PartialView("_Notification").WithInfo(new List<string> { "" });

            if (!ModelState.IsValid)
            {
                errorList = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => s.Value.Errors[0].ErrorMessage).ToList();
            }
            var Data = new
            {
                Success = !ModelState.IsValid ? false : true,
                Code = !ModelState.IsValid ? 500 : 200,
                Message = !ModelState.IsValid ? "Combination of part and location exists!" : "Test Master Created Successfuly",
                error = errorList
            };
            return Json(Data, JsonRequestBehavior.AllowGet);

            //TestMasterMapping objestMasterMapping = null;

            //objestMasterMapping = Mapper.Map<TestMasterMappingViewModel, TestMasterMapping>(testMasterMappingViewModel);
            //testMasterMappingService.IfExistsPartAndLocationCombination(objestMasterMapping);
            //var Data = new
            //{
            //    Success = true

            //};
            //return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
       
        public ActionResult CreateTestMaster(TestMasterViewModel testMasterModel)
        {
            _iTestMaserClient.AddTestMaster(testMasterModel);
            //if (ModelState.IsValid)
            //{
            //    //return PartialView("_Notification").WithSuccess(new List<string> { "Test Master Created Successfuly" });
            //    return Json(new
            //    {
            //     testMasterId = testMasterModel.TestMasterID
            //    }).WithSuccess(new List<string> { "Test Master Created Successfuly" });
            //}
            //else
            //{
            //    return PartialView("_Notification").WithValidationError(
            //        ModelState.Where(s => s.Value.Errors.Count > 0).SelectMany(s => new List<string> { s.Value.Errors[0].ErrorMessage }).ToList()
            //        );
            //}

            var Data = new
            {
                Success = !ModelState.IsValid ? false : true,
                Code = !ModelState.IsValid ? 500 : 200,
                Message = !ModelState.IsValid ? "error in creating test master" : "Test Master Created Successfuly",
                error = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new { s.Key, s.Value.Errors[0].ErrorMessage }).ToList()
            };

            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///    Get all part and location mapping for testmaster
        /// </summary>
        /// <param name="testMasterMappingViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [HandleBusinessException]
        public JsonResult CreateTestMasterMapping(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            _iTestMaserClient.CreateTestMasterMapping(testMasterMappingViewModel);
            if (ModelState.IsValid)
            {
                var Data = new
                {
                    Success = true,
                    testMasterId = testMasterMappingViewModel.TestMasterID,
                    msg = "location and part has been created!"
                };
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult Error()
        {
            return View("Error");
        }

        /// <summary>
        ///    Get all part and location mapping for testmaster
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult GetAllLocationAndPartMasterMapping(int testMasterid,bool redirect)
        {
            var masterMappingLocationAndParameterList = _iTestMaserClient.GetAllLocationAndPartMasterMapping(testMasterid,redirect);
            var jsonData = new { rows = masterMappingLocationAndParameterList };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// TO GET ALL PART MASTERS
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllLocationMaster()
        {
            var objLocationMasterViewModel = _iTestMaserClient.GetAllLocationMaster();

            IEnumerable<SelectListItem> locationList = new List<SelectListItem>();
            locationList = objLocationMasterViewModel.AsEnumerable().Select(testItem => new SelectListItem() { Text = testItem.LocationName, Value = testItem.LocationMasterID.ToString() });
            return Json(locationList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// TO GET ALL PART MASTERS
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllPartMaster()
        {
            var objGetTestMasterByTestIdViewModel = _iTestMaserClient.GetAllPartMaster();

            IEnumerable<SelectListItem> testsList = new List<SelectListItem>();
            //use of helper method to create select list item
            testsList = SelectListItemAdapter.GetSelectListItemCollection(objGetTestMasterByTestIdViewModel, s => s.PartNumber,s=>Convert.ToString(s.PartMasterID));
            return Json(testsList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllTest(string name = null)
        {
            List<TestsViewModel> testsViewModelList;
            testsViewModelList = _iTestClient.GetAllTest();
            IEnumerable<SelectListItem> testsList = new List<SelectListItem>();
            testsList = SelectListItemAdapter.GetSelectListItemCollection(testsViewModelList, s => s.TestName, s => Convert.ToString(s.TestID));
            //testsList = testsViewModelList.AsEnumerable().Select(testItem => new SelectListItem() { Text = testItem.TestName, Value = testItem.TestID.ToString() });
            return Json(testsList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///    Get all testmaster for particular test
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult GetAllTestMastersByTest(int testid,bool redirect)
        {
            var objGetTestMasterByTestIdViewModel = _iTestMaserClient.GetAllTestMastersByTest(testid,redirect);

            var jsonData = new { rows = objGetTestMasterByTestIdViewModel };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string name = null)
        {
            var testMaster = _iTestMaserClient.GetAll();
            return View(testMaster);
        }

        /// <summary>
        ///    Get all part and location mapping for testmaster
        /// </summary>
        /// <param name="testMasterMappingViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemovePartAndLocation(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            _iTestMaserClient.RemovePartAndLocation(testMasterMappingViewModel);
            var Data = new
            {
                Success = ModelState.IsValid ? true : false,
                testMasterId = testMasterMappingViewModel.TestMasterID,
                msg = ModelState.IsValid ? "location and part mapping removed!" : "Error in removing part and location mapping"
            };

            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///    Get all testmaster for particular test
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [HandleBusinessException]
        public JsonResult UpdateTestMaster(TestMasterViewModel testMasterViewModel)
        {
            _iTestMaserClient.UpdateTestMaster(testMasterViewModel);

            //TestMaster testMaster = null;
            //testMaster = Mapper.Map<TestMasterViewModel, TestMaster>(testMasterViewModel);
            //testMasterService.UpdateTestMaster(testMaster);
            //iUnitOfWork.Commit();
            var Data = new
            {
                Success = !ModelState.IsValid ? false : true,
                testMasterId = testMasterViewModel.TestMasterID,
                testMasterName = testMasterViewModel.TestMasterName,
                testMasterDescription = testMasterViewModel.Description,
                Code = !ModelState.IsValid ? 500 : 200,
                Message = (testMasterViewModel.operation == "Delete") ? "Test Master Removed" : "Test Master Updated!",
                error = ModelState.Where(s => s.Value.Errors.Count > 0).Select(s => new { s.Key, s.Value.Errors[0].ErrorMessage }).ToList()
            };
            //var Data = new
            //{
            //    Success = true,
            //    testMasterId = testMasterViewModel.TestMasterID,
            //    testMasterName = testMasterViewModel.TestMasterName,
            //    testMasterDescription = testMasterViewModel.Description,
            //    msg = (testMasterViewModel.operation == "Delete") ? "Test Master Removed" : "Test Master Updated!"
            //};
            return Json(Data, JsonRequestBehavior.AllowGet);
            //return null;
        }
        [HandleBusinessException]
        public ActionResult ManageTestMaster(int testMasterid)
        {
            //var area = this.RouteData.DataTokens["area"].ToString();
            return RedirectToAction<TestsController>(x => x.Index(testMasterid));
            //return RedirectToAction("Index", "Tests", new { Area = area, testMasterid = testMasterid });
     
        }
    }
}