
using cBaseQMS.Areas.cBaseQMS.Models;
using cBaseQMS.Areas.RestController;
using cBaseQMS.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cBaseQMS.Areas.cBaseQMS.RestController
{

    public interface ITestClientService
    {
        ModelStateDictionary ModelState { set; }
        List<TestsViewModel> GetAllTest();
         bool CreateTest(TestsViewModel testsViewModel);
        List<TestsViewModel> GetAllTestByTestMasterID(int testMasterId,string active, bool redirect);
        List<TestsViewModel> GetAllTestMasters();
        bool ShiftSequence(int currentKeyID, int nextKeyID, int prevKeyID, string opCode,int testMasterId);
        bool RemoveFromTestMaster(TestsViewModel testsViewModel);
        bool UpdateTest(TestsViewModel testsViewModel);
        bool MoveTest(TestsViewModel testsViewModel, int TestMasterIdFrom);
    }
    public class TestClient : ITestClientService
    {
       
        private readonly IRestClientBase iRestClientBase;
        public TestClient(IRestClientBase RestClientBase)
        {
            this.iRestClientBase = RestClientBase;
           
        }

        ModelStateDictionary ITestClientService.ModelState
        {
            set { this.iRestClientBase.MODELSTATE = value; }
        }

        public List<TestsViewModel> GetAllTest()
        {
            RestRequest request = new RestRequest("/GetAllTest");
            return this.iRestClientBase.Execute<List<TestsViewModel>>(request);
        }

        public List<TestsViewModel> GetAllTestMasters()
        {
            RestRequest request = new RestRequest("/GetAllTestMasters");
            return this.iRestClientBase.Execute<List<TestsViewModel>>(request);
        }
        public List<TestsViewModel> GetAllTestByTestMasterID(int testMasterId,string active, bool redirect)
        {
            RestRequest request = new RestRequest("/GetAllTestByTestMasterID");
            request.AddQueryParameter("testMasterId", testMasterId.ToString());
            request.AddQueryParameter("active", active.ToString());
            request.AddQueryParameter("redirect", redirect.ToString());
            return this.iRestClientBase.Execute<List<TestsViewModel>>(request);
        }

        public bool CreateTest(TestsViewModel testsViewModel)
        {
            RestRequest request = new RestRequest("/CreateTest", Method.POST);
            request.AddJsonBody(testsViewModel);
            this.iRestClientBase.Execute(request);
            return true;
        }

        public bool ShiftSequence(int currentKeyID, int nextKeyID, int prevKeyID, string opCode, int testMasterId)
        {
            RestRequest request = new RestRequest("/ShiftSequence", Method.POST);
            request.AddQueryParameter("currentKeyID", currentKeyID.ToString());
            request.AddQueryParameter("nextKeyID", nextKeyID.ToString());
            request.AddQueryParameter("prevKeyID", prevKeyID.ToString());
            request.AddQueryParameter("opCode", opCode.ToString());
            request.AddQueryParameter("testMasterId", testMasterId.ToString());
            this.iRestClientBase.Execute(request);
            return true;
        }

        public bool RemoveFromTestMaster(TestsViewModel testsViewModel)
        {
            RestRequest request = new RestRequest("/RemoveFromTestMaster", Method.POST);
            request.AddJsonBody(testsViewModel);          
            this.iRestClientBase.Execute(request);
            return true;
        }

        public bool UpdateTest(TestsViewModel testsViewModel)
        {
            RestRequest request = new RestRequest("/UpdateTest", Method.POST);
            request.AddJsonBody(testsViewModel);
            this.iRestClientBase.Execute(request);
            return true;
        }
        public bool MoveTest(TestsViewModel testsViewModel, int TestMasterIdFrom)
        {
            RestRequest request = new RestRequest("/MoveTest", Method.POST);
            request.AddJsonBody(testsViewModel);
            request.AddQueryParameter("TestMasterIdFrom", TestMasterIdFrom.ToString());
            this.iRestClientBase.Execute(request);
            return true;
        }
    }
    
}