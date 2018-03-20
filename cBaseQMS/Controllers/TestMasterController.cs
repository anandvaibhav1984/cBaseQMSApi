using cBaseQms.Service.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cBaseQMS.Models;
using AutoMapper;
using cBaseQMS.Mappings;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using Elmah;

namespace cBaseQMS.Controllers
{
    public class TestMasterController : Controller
    {
      
        private readonly ITestMasterService _testMasterService;
        private readonly IUnitOfWork _iUnitOfWork;
        
        public TestMasterController(ITestMasterService testMasterService, IUnitOfWork iunit)
        {
            this._testMasterService = testMasterService;
            this._iUnitOfWork = iunit;
        }
        // GET: TestMaster
        public ActionResult Index(string name=null)
        {
            IEnumerable<TestMasterViewModel> testMasterViewModel;
            IEnumerable<TestMaster> testMaster;

            testMaster = _testMasterService.GetTests(name).ToList();
            testMasterViewModel = Mapper.Map<IEnumerable<TestMaster>, IEnumerable<TestMasterViewModel>>(testMaster);
            return View(testMasterViewModel);
        }

        //[HttpPost]
        public ActionResult saveTestMaster(TestMasterViewModel testMasterModel)
        {
            try
            {
                TestMaster objTestMaster = null;
                objTestMaster = Mapper.Map<TestMasterViewModel, TestMaster>(testMasterModel);
                _testMasterService.CreateCategory(objTestMaster);
                _iUnitOfWork.Commit();
            }
            catch(Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);

            }
            return RedirectToAction("Index","TestMaster");
        }
        
        public ActionResult Create(TestMasterViewModel testMaster)
        {
          
            return View();
        }
    }
}