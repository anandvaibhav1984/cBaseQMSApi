using AutoMapper;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using cBaseQms.Repository.Repositories;
using cBaseQMS.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace cBaseQms.Service.Services
{
    // operations you want to expose
    public interface ITestMasterService
    {
        IEnumerable<TestMasterViewModel> GetMasterTests(string name = null);

        TestMaster GetTestMaster(int id);

        TestMaster GetTestMaster(string name);

        void SaveTestMaster();

        IEnumerable<TestMaster> GetMasterTests(object[] param);

        void CreateTestMaster(TestMasterViewModel testMaster);

        void UpdateTestMaster(TestMasterViewModel testMaster);

        IEnumerable<usp_GetTestMasterByTestIdViewModel> GetTestMasterByTestId(int testid);
    }

    public class TestMasterService : ITestMasterService
    {
        private readonly ITestMasterRepository testMasterRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _iMapper;

        public TestMasterService(ITestMasterRepository testMasterRepository, IUnitOfWork unitOfWork, IMapper _iMapper)
        {
            this.testMasterRepository = testMasterRepository;
            this.unitOfWork = unitOfWork;
            this._iMapper = _iMapper;
        }

        void ITestMasterService.CreateTestMaster(TestMasterViewModel testMasterViewModel)
        {
            TestMaster testMaster;
            testMaster = _iMapper.Map<TestMasterViewModel, TestMaster>(testMasterViewModel);
            testMasterRepository.Add(testMaster);
        }

        TestMaster ITestMasterService.GetTestMaster(int id)
        {
            var category = testMasterRepository.GetById(id);
            return category;
        }

        TestMaster ITestMasterService.GetTestMaster(string name)
        {
            var category = testMasterRepository.GetTestMasterByName(name);
            return category;
        }

       
        IEnumerable<TestMasterViewModel> ITestMasterService.GetMasterTests(string name)
        { 
            IEnumerable <TestMasterViewModel> testMasterViewModel;
            IEnumerable<TestMaster> testMaster;

            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<ViewModelProfile>();

            //});

            if (string.IsNullOrEmpty(name))
                testMaster= testMasterRepository.GetAll().Where(c => c.IsActive == true).ToList();
            else
                testMaster= testMasterRepository.GetAll().Where(c => c.TestMasterName == name && c.IsActive == true).ToList();
            testMasterViewModel = _iMapper.Map<IEnumerable<TestMaster>, IEnumerable<TestMasterViewModel>>(testMaster);
            //testMasterViewModel = config.CreateMapper().Map<IEnumerable<TestMaster>, IEnumerable<TestMasterViewModel>>(testMaster);
            return testMasterViewModel;
        }

        void ITestMasterService.SaveTestMaster()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<TestMaster> GetMasterTests(object[] param)
        {
            string spName = "usp_GetTestMasterByTestId {0}";
            return testMasterRepository.ExecuteQuery(spName, param);
        }

        public void UpdateTestMaster(TestMasterViewModel testMasterViewModel)
        {
            
            TestMaster testMaster;
            testMaster = _iMapper.Map<TestMasterViewModel, TestMaster>(testMasterViewModel);
            testMasterRepository.Update(testMaster, o => o.TestMasterName, o => o.IsActive, o => o.ModifiedBy, o => o.ModifiedOn);
        }

        public IEnumerable<usp_GetTestMasterByTestIdViewModel> GetTestMasterByTestId(int testid)
        {
            IEnumerable<usp_GetTestMasterByTestIdViewModel> testMasterViewModel;
            IEnumerable<usp_GetTestMasterByTestId> testMaster;
            testMaster = testMasterRepository.getGetTestMasterByTestId(testid).ToList();
            testMasterViewModel = _iMapper.Map<IEnumerable<usp_GetTestMasterByTestId>, IEnumerable<usp_GetTestMasterByTestIdViewModel>>(testMaster);
            return testMasterViewModel;
        }

     
    }
}