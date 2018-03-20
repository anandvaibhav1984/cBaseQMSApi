using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using cBaseQMS.Models;
using cBaseQMS.Areas.RestController;
using cBaseQMS.Api.Models;
using cBaseQMS.Areas.cBaseQMS.Models;

namespace cBaseQMS.Areas.cBaseQMS.RestController
{

    public interface ITestMasterClient
    {
         ModelStateDictionary ModelState {  set; }
       // ModelStateDictionary ModelState { get; set; }
        List<TestMasterViewModel> GetAll();
        List<usp_GetTestMasterByTestIdViewModel> GetAllTestMastersByTest(int testid,bool redirect);
        List<PartMasterViewModel> GetAllPartMaster();
        List<LocationMasterViewModel> GetAllLocationMaster();
        List<usp_GetLocationAndPartMappingViewModel> GetAllLocationAndPartMasterMapping(int testMasterid, bool redirect);
        void AddTestMaster(TestMasterViewModel testMasterViewModel);
        void ifExistsLocationAndPartMapping(TestMasterMappingViewModel testMasterMappingViewModel);
        void CreateTestMasterMapping(TestMasterMappingViewModel testMasterMappingViewModel);
        void RemovePartAndLocation(TestMasterMappingViewModel testMasterMappingViewModel);
        void UpdateTestMaster(TestMasterViewModel testMasterViewModel);
    }


    public class TestMasterClient :  ITestMasterClient
    {
       

        private readonly IRestClientBase iRestClientBase;
        public TestMasterClient( IRestClientBase RestClientBase) 
        {
            this.iRestClientBase = RestClientBase;
        }

         ModelStateDictionary ITestMasterClient.ModelState
        {
            set { this.iRestClientBase.MODELSTATE=value; } }

        public List<TestMasterViewModel> GetAll()
        {
            RestRequest request = new RestRequest("/GetAllTestMaster");
            return this.iRestClientBase.Execute<List<TestMasterViewModel>>(request);
        }


        public void AddTestMaster(TestMasterViewModel testMasterViewModel)
        {
            RestRequest request = new RestRequest("/addTestMaster", Method.POST);
            request.AddJsonBody(testMasterViewModel);
            this.iRestClientBase.Execute(request);
        }


        public void CreateTestMasterMapping(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            RestRequest request = new RestRequest("/CreateTestMasterMapping", Method.POST);
            request.AddJsonBody(testMasterMappingViewModel);
            this.iRestClientBase.Execute(request);
        }

        public List<usp_GetTestMasterByTestIdViewModel> GetAllTestMastersByTest(int testid,bool redirect )
        {
            RestRequest request = new RestRequest("/GetAllTestMasterByTest", Method.GET);
            request.AddQueryParameter("testid", testid.ToString());
            request.AddQueryParameter("redirect", redirect.ToString());
            return this.iRestClientBase.Execute<List<usp_GetTestMasterByTestIdViewModel>>(request);
        }

        public List<PartMasterViewModel> GetAllPartMaster()
        {
            RestRequest request = new RestRequest("/GetAllPartMaster");

            return this.iRestClientBase.Execute<List<PartMasterViewModel>>(request);
        }

        public List<LocationMasterViewModel> GetAllLocationMaster()
        {
            RestRequest request = new RestRequest("/GetAllLocationMaster");

            return this.iRestClientBase.Execute<List<LocationMasterViewModel>>(request);
        }

        public List<usp_GetLocationAndPartMappingViewModel> GetAllLocationAndPartMasterMapping(int testMasterid, bool redirect)
        {
            RestRequest request = new RestRequest("/GetAllLocationAndPartMasterMapping");
            request.AddQueryParameter("testMasterid", testMasterid.ToString());
            request.AddQueryParameter("redirect", redirect.ToString());
            return this.iRestClientBase.Execute<List<usp_GetLocationAndPartMappingViewModel>>(request);
        }

        public void ifExistsLocationAndPartMapping(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            RestRequest request = new RestRequest("/ifExistsLocationAndPartMapping", Method.POST);
            request.AddJsonBody(testMasterMappingViewModel);
            this.iRestClientBase.Execute<List<TestMasterMappingViewModel>>(request);
        }

        public void RemovePartAndLocation(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            RestRequest request = new RestRequest("/RemovePartAndLocation", Method.POST);
            request.AddJsonBody(testMasterMappingViewModel);
            this.iRestClientBase.Execute<List<TestMasterMappingViewModel>>(request);
        }

        public void UpdateTestMaster(TestMasterViewModel testMasterViewModel)
        {
            RestRequest request = new RestRequest("/UpdateTestMaster", Method.POST);
            request.AddJsonBody(testMasterViewModel);
            this.iRestClientBase.Execute<List<TestMasterMappingViewModel>>(request);
        }
    }

 
  
}