using AutoMapper;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using cBaseQms.Repository.Repositories;
using cBaseQMS.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cBaseQms.Service.Services
{
    // operations you want to expose
    public interface ITestService
    {
        IEnumerable<TestsViewModel> GetAllTests(string name = null);
        IEnumerable<TestsViewModel> GetAllTestByTestMasterID(int testMasterId,string active);
        bool ShiftSequence(int currentKeyID, int nextKeyID, int prevKeyID, string opCode,int testMasterId);
        Test GetTest(int id);
        Test GetTest(string name);
        bool CreateTest(TestsViewModel test);
        void SaveTest();
        bool RemoveFromTestMaster(TestsViewModel testsViewModel);
        bool UpdateTest(TestsViewModel testsViewModel);
        bool MoveTest(TestsViewModel testsViewModel, int TestMasterIdFrom);

    }
    public class TestService : ITestService
    {
        private readonly ITestRepository testRepository;        
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _iMapper;
        public TestService(ITestRepository testRepository, IUnitOfWork _iUnitOfWork, IMapper _iMapper)
        {   
            this.testRepository = testRepository;
            this._iUnitOfWork = _iUnitOfWork;
            this._iMapper = _iMapper;

        }

        public bool CreateTest(TestsViewModel testsViewModel)
        { 
            Test test;
            IEnumerable<TestsViewModel> testsViewModelList;
            testsViewModelList = GetAllTestByTestMasterID(testsViewModel.TestMasterID,"true");
            int maxSequence = testsViewModelList.Count() > 0 ?testsViewModelList.Max(o => o.Sequence)+1:1;
            testsViewModel.Sequence = maxSequence;
            test = _iMapper.Map<TestsViewModel, Test>(testsViewModel);

            testRepository.Add(test);
          //  throw new Exception("this is ex");
            SaveTest();
           
            return test.TestID > 0;
            
        }

        public IEnumerable<TestsViewModel> GetAllTestByTestMasterID(int testMasterId, string active)
        {
            IEnumerable<TestsViewModel> testsViewModel;
            IEnumerable<Test> test;
            bool objActive;
            if (Boolean.TryParse(active,out objActive))
            {
                test = testRepository.GetAll().Where(c => c.TestMasterID == testMasterId && c.IsActive== objActive).OrderBy(e=>e.Sequence).ToList();
            }
            else
                test = testRepository.GetAll().Where(c => c.TestMasterID == testMasterId ).OrderBy(e => e.Sequence).ToList();
            testsViewModel = _iMapper.Map<IEnumerable<Test>, IEnumerable<TestsViewModel>>(test);
            return testsViewModel;
        }

        public IEnumerable<TestsViewModel> GetAllTests(string name = null)
        {
            IEnumerable<TestsViewModel> testsViewModel;
            IEnumerable<Test> test;
            
            if (string.IsNullOrEmpty(name))
                test = testRepository.GetAll().ToList();
            else
                test = testRepository.GetAll().Where(c => c.TestName== name).ToList();

            testsViewModel = _iMapper.Map<IEnumerable<Test>, IEnumerable<TestsViewModel>>(test);
            return testsViewModel;
        }

        public Test GetTest(int id)
        {
            throw new NotImplementedException();
        }

        public Test GetTest(string name)
        {
            var test = testRepository.GetTests(name);
            return test;
        }

        public bool MoveTest(TestsViewModel testsViewModel, int TestMasterIdFrom)
        {

            this.CreateTest(testsViewModel);
          
            testsViewModel.TestID = TestMasterIdFrom;
            Test test;
            test = _iMapper.Map<TestsViewModel, Test>(testsViewModel);
            testRepository.Update(test, o => o.IsActive, o => o.ModifiedBy, o => o.ModifiedOn);
            SaveTest();
            return true;

        }

        public bool RemoveFromTestMaster(TestsViewModel testsViewModel)
        {
            Test test;
            test = _iMapper.Map<TestsViewModel, Test>(testsViewModel);
            testRepository.Update(test, o => o.IsActive, o => o.ModifiedBy, o => o.ModifiedOn);
            SaveTest();
            return true;
        }

        public bool UpdateTest(TestsViewModel testsViewModel)
        {
            Test test;
            test = _iMapper.Map<TestsViewModel, Test>(testsViewModel);
            testRepository.Update(test, o => o.TestName, o => o.IsActive, o => o.ModifiedBy, o => o.ModifiedOn);
            SaveTest();
            return true;
        }

        public void SaveTest()
        {
            _iUnitOfWork.Commit();
        }

        public bool ShiftSequence(int currentKeyID, int nextKeyID, int prevKeyID, string opCode, int testMasterId)
        {
            IEnumerable<Test> test;
            test = testRepository.GetAll().Where(c => c.TestMasterID == testMasterId).OrderBy(e => e.Sequence).ToList();
            if (opCode == "UP" && prevKeyID>0)
            {
                var currentTest = test.Where(x => x.TestID == currentKeyID).FirstOrDefault();
                currentTest.Sequence = test.Where(x => x.TestID == currentKeyID).Select(s => s.Sequence).FirstOrDefault() - 1;
                testRepository.Update(currentTest, o => o.Sequence);

                var previousTest = test.Where(x => x.TestID == prevKeyID).FirstOrDefault();
                previousTest.Sequence = test.Where(x => x.TestID == prevKeyID).Select(s => s.Sequence).FirstOrDefault() + 1;
                testRepository.Update(previousTest, o => o.Sequence);
                SaveTest();

            }

            else if (opCode == "DW" && nextKeyID>0)
            {
                var currentTest = test.Where(x => x.TestID == currentKeyID).FirstOrDefault();
                currentTest.Sequence = test.Where(x => x.TestID == currentKeyID).Select(s => s.Sequence).FirstOrDefault() +1;
                testRepository.Update(currentTest, o => o.Sequence);

                var previousTest = test.Where(x => x.TestID == nextKeyID).FirstOrDefault();
                previousTest.Sequence = test.Where(x => x.TestID == nextKeyID).Select(s => s.Sequence).FirstOrDefault() - 1;
                testRepository.Update(previousTest, o => o.Sequence);
                SaveTest();

            }
            return true;
        }
    }
}
