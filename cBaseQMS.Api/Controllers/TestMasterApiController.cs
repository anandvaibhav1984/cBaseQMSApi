
using cBaseQMS.Service.Models;
using cBaseQms.Repository.Infrastructure;
using cBaseQms.Service.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cBaseQMS.Api.Caching;

namespace cBaseQMS.Api.Controllers
{
    [Route("TestMaster")]
    public class TestMasterApiController : ApiController
    {
        

        private readonly ITestMasterService iTestMasterService;
        private readonly IPartMasterService iPartMasterService;
        private readonly ILocationMasterService iLocationMasterService;
        private readonly ITestMasterMappingService itestMasterMappingService;
        private readonly ICacheService cache;

        public TestMasterApiController(IUnitOfWork iunit, ITestMasterService iTestMasterService, 
            IPartMasterService iPartMasterService, ILocationMasterService iLocationMasterService, ITestMasterMappingService itestMasterMappingService, ICacheService cache)
        {
            this.itestMasterMappingService = itestMasterMappingService;
            this.iLocationMasterService = iLocationMasterService;
            this.iPartMasterService = iPartMasterService;
            this.iTestMasterService = iTestMasterService;
            this.cache = cache;


        }

        [Route("GetAllTestMaster")]
        public IHttpActionResult Get()
        {

            IEnumerable<TestMasterViewModel> testMasterViewModel;           
            testMasterViewModel =cache.GetOrSet("GetAllTestMaster", ()=>iTestMasterService.GetMasterTests().ToList());           
            return Ok(testMasterViewModel);

        }

        [Route("GetAllTestMasterByTest")]
        public IHttpActionResult GetAllTestMasterByTest(int testid,string redirect )
        {

            IEnumerable<usp_GetTestMasterByTestIdViewModel> testMasterViewModel;
            testMasterViewModel = cache.GetOrSet("GetAllTestMasterByTest", () => iTestMasterService.GetTestMasterByTestId(testid),bool.Parse(redirect));
            return Ok(testMasterViewModel);

        }


        [HttpPost]
        [Route("AddTestMaster")]
        public IHttpActionResult AddTestMaster(TestMasterViewModel testMasterViewModel)
        {
            
            iTestMasterService.CreateTestMaster(testMasterViewModel);
            iTestMasterService.SaveTestMaster();
            return Ok();

        }

        [Route("GetAllPartMaster")]
        public IHttpActionResult GetAllPartMaster()
        {
            IEnumerable<PartMasterViewModel> partMasterViewModel;
            partMasterViewModel = cache.GetOrSet("GetAllPartMaster", ()=>iPartMasterService.GetAllParts().ToList());
            return Ok(partMasterViewModel);

        }

        [Route("GetAllLocationMaster")]
        public IHttpActionResult GetAllLocationMaster()
        {
            List<LocationMasterViewModel> locationMasterViewModel;
            locationMasterViewModel =cache.GetOrSet("GetAllLocationMaster", ()=> iLocationMasterService.GetAllLocations().ToList());
            return Ok(locationMasterViewModel);
        }


        [Route("GetAllLocationAndPartMasterMapping")]
        public IHttpActionResult GetAllLocationAndPartMasterMapping(int testMasterid,string redirect)
        {
            List<Usp_GetLocationAndPartMappingViewModel> objgetAllLocationAndPartMasterMapping;
            objgetAllLocationAndPartMasterMapping = cache.GetOrSet("GetAllLocationAndPartMasterMapping",()=> itestMasterMappingService.GetAllLocationAndPartMasterMapping(testMasterid),bool.Parse(redirect)).ToList();
            return Ok(objgetAllLocationAndPartMasterMapping);
        }

        [Route("ifExistsLocationAndPartMapping")]
        public IHttpActionResult ifExistsLocationAndPartMapping(TestMasterMappingViewModel testMasterMappingViewModel)
        {   
            return Ok();
        }

        [Route("CreateTestMasterMapping")]
        public IHttpActionResult CreateTestMasterMapping(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            itestMasterMappingService.CreateTestMasterMapping(testMasterMappingViewModel);
            itestMasterMappingService.SaveTest();
            return Ok();
        }

        [Route("RemovePartAndLocation")]
        public IHttpActionResult RemovePartAndLocation(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            itestMasterMappingService.RemovePartAndLocationMapping(testMasterMappingViewModel);
            itestMasterMappingService.SaveTest();
            return Ok();
        }


        [HttpPost]
        [Route("UpdateTestMaster")]
        public IHttpActionResult UpdateTestMaster(TestMasterViewModel testMasterViewModel)
        {
            iTestMasterService.UpdateTestMaster(testMasterViewModel);
            iTestMasterService.SaveTestMaster();
            return Ok();

        }


    }
}
