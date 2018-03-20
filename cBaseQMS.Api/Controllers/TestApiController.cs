
using cBaseQms.Repository.Infrastructure;
using cBaseQms.Service.Services;
using cBaseQMS.Api.Caching;
using cBaseQMS.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace cBaseQMS.Api.Controllers
{
    [Route("Test")]
    public class TestApiController : ApiController
    {
        private readonly ITestService _iTestService;
        private ICacheService _cache;

        public TestApiController(ITestService _iTestService, ICacheService cache)
        {
            this._iTestService = _iTestService;
            this._cache = cache;
        }

        [Route("GetAllTest")]
        public IHttpActionResult GetAllTests()
        {
            IEnumerable<TestsViewModel> testsViewModel;
            testsViewModel = _cache.GetOrSet("GetAllTests",()=>_iTestService.GetAllTests().ToList());
            return Ok(testsViewModel);
        }

        [Route("GetAllTestByTestMasterID")]
        public IHttpActionResult GetAllTestByTestMasterID(int testMasterId,string active, string redirect)
        {
            IEnumerable<TestsViewModel> testsViewModel;
            testsViewModel = _cache.GetOrSet("GetAllTestByTestMasterID", ()=>_iTestService.GetAllTestByTestMasterID(testMasterId,active).ToList(),bool.Parse(redirect));
            return Ok(testsViewModel);  
        }

        [Route("CreateTest")]
        public IHttpActionResult CreateTest(TestsViewModel testsViewModel)
        {           
            var status =_iTestService.CreateTest(testsViewModel);            
            return Ok(status);
        }


        [Route("ShiftSequence")]
        public IHttpActionResult ShiftSequence(int currentKeyID, int nextKeyID, int prevKeyID, string opCode, int testMasterId)
        {
            var status = _iTestService.ShiftSequence(currentKeyID,nextKeyID,prevKeyID, opCode,testMasterId);
            return Ok(status);
        }

        [Route("RemoveFromTestMaster")]
        public IHttpActionResult RemoveFromTestMaster(TestsViewModel testsViewModel)
        {
            var status = _iTestService.RemoveFromTestMaster(testsViewModel);
            return Ok(status);
        }

        [Route("UpdateTest")]
        public IHttpActionResult UpdateTest(TestsViewModel testsViewModel)
        {
            var status = _iTestService.UpdateTest(testsViewModel);
            return Ok(status);
        }

        [Route("MoveTest")]
        public IHttpActionResult MoveTest(TestsViewModel testsViewModel, int TestMasterIdFrom)
        {
            var status = _iTestService.MoveTest(testsViewModel,TestMasterIdFrom);
            return Ok(status);
        }
    }
}