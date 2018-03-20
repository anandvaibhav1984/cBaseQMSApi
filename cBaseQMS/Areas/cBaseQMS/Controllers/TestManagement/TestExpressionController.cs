
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cBaseQMS.Models;



using Elmah;
using cBaseQMS.Areas.cBaseQMS.Common;
using Newtonsoft.Json;

namespace cBaseCore.Areas.cBaseQMS.Controllers.TestManagement
{
    public class TestExpressionController : Controller
    {
        //private readonly ITestExpressionService testExpService;
        //private readonly IUnitOfWork iUnitOfWork;

        ///// <summary>
        ///// dependency injected thru constructor
        ///// </summary>
        ///// <param name="testExpressionService"></param>
        ///// <param name="iunit"></param>
        //public TestExpressionController(ITestExpressionService testExpressionService, IUnitOfWork iunit)
        //{
        //    this.testExpService = testExpressionService;
        //    this.iUnitOfWork = iunit;
        //}

        //// GET: cBaseQMS/TestExpression
        //public ActionResult Index()
        //{
        //    TestExpressionViewModel expModel = PopulateDDL();
        //    return View(expModel);
        //}

        ///// <summary>
        ///// Load Tests By TestMasterId
        ///// </summary>
        ///// <param name="testMasterId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult LoadTestsByTestMasterId(int testMasterId)
        //{
        //    bool success = false;
        //    //fetch tests based on test master id
        //    var testList = testExpService.GetTestsByTestMasterId(testMasterId);

        //    if (testList != null && testList.Count() > 0)
        //    {
        //        success = (testList != null);
        //        //get text and values from tests
        //        var items = testList.Select(x => new
        //        {
        //            text = x.TestName,
        //            value = x.TestID
        //        });
                
        //        return Json(new { success = success, results = items }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new { success = success }, JsonRequestBehavior.AllowGet);
        //}
        ///// <summary>
        ///// load all component items
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="testMasterId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult LoadComponentItems(string type, int testMasterId, int testId)
        //{
        //    bool success = false;
        //    JsonResult jsonReturn = null;
        //    //sng dwnld
        //    switch (type)
        //    {
        //        case (TestExpressionViewModel.Test):
        //            var testList = testExpService.GetTestsByComponent(testMasterId);
        //            if (testList != null && testList.Count() > 0)
        //            {
        //                success = (testList != null);
        //                //get text and values from tests
        //                var items = testList.Select(x => new
        //                {
        //                    text = x.TestName,
        //                    value = x.TestID
        //                });

        //                jsonReturn = Json(new { success = success, results = items }, JsonRequestBehavior.AllowGet);
        //            }
        //            break;
        //        case (TestExpressionViewModel.Condition):
        //            break;
        //        case (TestExpressionViewModel.Equations):
        //            var equationList = testExpService.GetEquationsByTestId(testId);
        //            if (equationList != null && equationList.Count() > 0)
        //            {
        //                success = (equationList != null);
        //                //get text and values from tests
        //                var items = equationList.Select(x => new
        //                {
        //                    text = x.EquationName,
        //                    value = x.EquationID
        //                });

        //                jsonReturn = Json(new { success = success, results = items }, JsonRequestBehavior.AllowGet);
        //            }
        //            break;
        //        case (TestExpressionViewModel.InputField):
        //            var inputFieldList = testExpService.GetInputFieldsByTestId(testId);
        //            if (inputFieldList != null && inputFieldList.Count() > 0)
        //            {
        //                success = (inputFieldList != null);
        //                //get text and values from tests
        //                var items = inputFieldList.Select(x => new
        //                {
        //                    text = x.FieldName,
        //                    value = x.InputFieldID
        //                });

        //                jsonReturn = Json(new { success = success, results = items }, JsonRequestBehavior.AllowGet);
        //            }
        //            break;
        //        case (TestExpressionViewModel.CalculatedField):
        //            var testCalFieldList = testExpService.GetTestCalculatedFieldsByTestId(testId);
        //            if (testCalFieldList != null && testCalFieldList.Count() > 0)
        //            {
        //                success = (testCalFieldList != null);
        //                //get text and values from tests
        //                var items = testCalFieldList.Select(x => new
        //                {
        //                    text = x.FieldName,
        //                    value = x.CalcFieldID
        //                });

        //                jsonReturn = Json(new { success = success, results = items }, JsonRequestBehavior.AllowGet);
        //            }
        //            break;
        //        case (TestExpressionViewModel.LocationAttribute):
        //            var locationAttList = testExpService.GetLocationAttributesByTestMasterId(testMasterId);
        //            if (locationAttList != null && locationAttList.Count() > 0)
        //            {
        //                success = (locationAttList != null);
        //                //get text and values from tests
        //                var items = locationAttList.Select(x => new
        //                {
        //                    text = x.LocAttrName,
        //                    value = x.Value
        //                });

        //                jsonReturn = Json(new { success = success, results = items }, JsonRequestBehavior.AllowGet);
        //            }
        //            break;
        //        case (TestExpressionViewModel.PartAttribute):
        //            var partAttributeList = testExpService.GetPartAttributesByTestId(testId);
        //            if (partAttributeList != null && partAttributeList.Count() > 0)
        //            {
        //                success = (partAttributeList != null);
        //                //get text and values from tests
        //                var items = partAttributeList.Select(x => new
        //                {
        //                    text = x.PartAttrName,
        //                    value = x.MaxValue
        //                });

        //                jsonReturn = Json(new { success = success, results = items }, JsonRequestBehavior.AllowGet);
        //            }
        //            break;
        //    }
        //    return jsonReturn;
        //}

        ///// <summary>
        ///// post of index
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult SaveTestDetails(Test testExp)
        //{
        //    bool success = false;
        //    if (testExp != null)
        //    {
        //        testExpService.UpdateTestExpression(testExp);
        //        success = (testExp != null);
        //    }
        //    return Json(new { success = success }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public JsonResult GetExpression(int testMasterId, int testId)
        //{
        //    bool success=false;
        //    //empty list for anonymous type
        //    var emptyList = new List<Tuple<string, string>>()
        //                            .Select(t => new { Id = t.Item1, Name = t.Item2 }).ToList();
        //    //getting test detail
        //    var tst = testExpService.GetTestsFromTestIdAndMasterId(testMasterId, testId);

            

        //    if (tst != null && !string.IsNullOrWhiteSpace(tst.Expression))
        //    {
                
        //        var tstExpression = tst.Expression.Split('~');
        //        if (tstExpression.Length > 0)
        //        {
        //            foreach (string exp in tstExpression)
        //            {
        //                if (exp.Contains("^"))
        //                {
        //                    emptyList.Add(new { Id = exp, Name = exp.Split('^')[0] });
        //                }
        //                else
        //                {
        //                    emptyList.Add(new { Id = exp, Name = exp });
        //                }
        //                success = true;
        //            }
        //        }
        //    }
            
        //    return Json(new { success = success, results = emptyList }, JsonRequestBehavior.AllowGet);
        //}
        //#region "Private Methods"
        ///// <summary>
        ///// PopulateDDL
        ///// </summary>
        ///// <returns></returns>
        //private TestExpressionViewModel PopulateDDL()
        //{
        //    TestExpressionViewModel expModel = new TestExpressionViewModel();
        //    expModel.TestMasterItems = testExpService.GetAllTestMasters().AsEnumerable()
        //                                            .Select(testItem => new SelectListItem() { Text = testItem.TestMasterName, Value = testItem.TestMasterID.ToString() }); ;

        //    expModel.ComponentItems = testExpService.GetAllComponents().AsEnumerable()
        //                                            .Select(comp => new SelectListItem() { Text = comp.ComponentName, Value = comp.ShortName }); ;
        //    return expModel;
        //}
       // #endregion
    }
}