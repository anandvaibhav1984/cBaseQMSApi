using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository;
using cBaseQms.Repository.Repositories;
using cBaseQms.Repository.Infrastructure;
using cBaseQMS.Service.Models;
using AutoMapper;

namespace cBaseQms.Service.Services
{
    // operations you want to expose
    public interface ITestMasterMappingService
    {
        IEnumerable<Usp_GetLocationAndPartMappingViewModel> GetAllLocationAndPartMasterMapping(int testmasterid);
        void CreateTestMasterMapping(TestMasterMappingViewModel testMasterMapping);
        void RemovePartAndLocationMapping(TestMasterMappingViewModel testMasterMapping);
        void UpdateTestMasterMapping(object[] param);
        void IfExistsPartAndLocationCombination(TestMasterMapping testMasterMapping);
        void SaveTest();
    }
    public class TestMasterMappingService : ITestMasterMappingService
    {
        private readonly ITestMasterMappingRepositry testMasterMappingRepositry;        
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _iMpper;


        public TestMasterMappingService(ITestMasterMappingRepositry testMasterMappingRepositry, IUnitOfWork unitOfWork,IMapper _iMpper)
        {
        
            this.testMasterMappingRepositry = testMasterMappingRepositry;
            this.unitOfWork = unitOfWork;
            this._iMpper = _iMpper;
        }

        public void UpdateTestMasterMapping(object[] param)
        {
            string spName = "usp_UpdateLocationAndPartMapping {0}";
            testMasterMappingRepositry.ExecuteCommand(spName, param);
        }

        
        IEnumerable<Usp_GetLocationAndPartMappingViewModel> ITestMasterMappingService.GetAllLocationAndPartMasterMapping(int testmasterid)
        {
            List<Usp_GetLocationAndPartMappingViewModel> objgetAllLocationAndPartMasterMapping;
            List<usp_GetLocationAndPartMapping> objPartlocationMasterMapping;
            objPartlocationMasterMapping = testMasterMappingRepositry.GetAllLocationAndPartMasterMapping(testmasterid);
            objgetAllLocationAndPartMasterMapping = _iMpper.Map<List<usp_GetLocationAndPartMapping>, List<Usp_GetLocationAndPartMappingViewModel>>(objPartlocationMasterMapping);
            return objgetAllLocationAndPartMasterMapping;
        }

        public void SaveTest()
        {
            unitOfWork.Commit();
        }

        public void CreateTestMasterMapping(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            TestMasterMapping testMasterMapping;
            testMasterMapping = _iMpper.Map<TestMasterMappingViewModel, TestMasterMapping>(testMasterMappingViewModel);
            testMasterMappingRepositry.Add(testMasterMapping);
        }

        public void RemovePartAndLocationMapping(TestMasterMappingViewModel testMasterMappingViewModel)
        {
            TestMasterMapping testMasterMapping;
            testMasterMapping = _iMpper.Map<TestMasterMappingViewModel, TestMasterMapping>(testMasterMappingViewModel);
            //Property based updation
            testMasterMappingRepositry.Update(testMasterMapping,o => o.IsActive,o=>o.ModifiedBy, o => o.ModifiedOn);
            
        }

        public void IfExistsPartAndLocationCombination(TestMasterMapping testMasterMapping)
        {
          
            
        }
    }
}
