using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository;
using cBaseQms.Repository.Repositories;
using cBaseQms.Repository.Infrastructure;

namespace cBaseQms.Service.Services
{
    public interface ITestExpressionService
    {
        IEnumerable<TestMaster> GetAllTestMasters();
        IEnumerable<Test> GetTestsByTestMasterId(int testMasterId);
        IEnumerable<Component> GetAllComponents();
        IEnumerable<Test> GetTestsByComponent(int testMasterId);
        IEnumerable<Equation> GetEquationsByTestId(int testId);
        IEnumerable<InputField> GetInputFieldsByTestId(int testId);
        IEnumerable<vWLocationAttribute> GetLocationAttributesByTestMasterId(int testMasterId);
        IEnumerable<PartAttribute> GetPartAttributesByTestId(int testId);
        IEnumerable<TestCalculatedField> GetTestCalculatedFieldsByTestId(int testId);
        int UpdateTestExpression(Test entity);
        Test GetTestsFromTestIdAndMasterId(int testMasterId, int testId);
    }
    public class TestExpressionService : ITestExpressionService
    {
        private readonly ITestExpressionRepository testExpressionRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITestMasterRepository testMasterRepository;
        private readonly ITestRepository testRepository;
        private readonly IComponentRepositry componentRepository;
        private readonly IEquationRepositry equationRepository;
        private readonly ITestInputFieldsRepositry inputFieldRepository;
        private readonly IVWLocationAttributesRepository vWLocationAttributesRepository;
        private readonly IPartAttributesRepository partAttributesRepository;
        private readonly ITestCalculatedFieldsRepository testCalulatedFieldsRepository;
        /// <summary>
        /// dependency injected thru constructor
        /// </summary>
        /// <param name="testExpMasterRepo"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="testMasRepo"></param>
        /// <param name="testRepo"></param>
        public TestExpressionService(ITestExpressionRepository testExpMasterRepo, IUnitOfWork unitOfWork, ITestMasterRepository testMasRepo
            , ITestRepository testRepo, IComponentRepositry componentRepo, IEquationRepositry equationRepo, ITestInputFieldsRepositry inputFieldRepo
            , IVWLocationAttributesRepository vWLocationAttributesRepo, IPartAttributesRepository partAttributesRepo
            , ITestCalculatedFieldsRepository testCalulatedFieldsRepo)
        {
            this.testExpressionRepository = testExpMasterRepo;
            this.unitOfWork = unitOfWork;
            this.testMasterRepository = testMasRepo;
            this.testRepository = testRepo;
            this.componentRepository = componentRepo;
            this.equationRepository = equationRepo;
            this.inputFieldRepository = inputFieldRepo;
            this.vWLocationAttributesRepository = vWLocationAttributesRepo;
            this.partAttributesRepository = partAttributesRepo;
            this.testCalulatedFieldsRepository = testCalulatedFieldsRepo;
        }
        /// <summary>
        /// Get All Test Masters
        /// </summary>
        /// <returns></returns>
        IEnumerable<TestMaster> ITestExpressionService.GetAllTestMasters()
        {
            return testMasterRepository.GetAll().ToList();
        }
        /// <summary>
        /// Get Tests By TestMasterId
        /// </summary>
        /// <param name="testMasterId"></param>
        /// <returns></returns>
        IEnumerable<Test> ITestExpressionService.GetTestsByTestMasterId(int testMasterId)
        {
            return testRepository.GetAll().Where(x => x.TestMasterID == testMasterId).ToList();
        }
        /// <summary>
        /// Get All Components
        /// </summary>
        /// <returns></returns>
        IEnumerable<Component> ITestExpressionService.GetAllComponents()
        {
            return componentRepository.GetAll().ToList();
        }
        /// <summary>
        /// Get Tests By Component
        /// </summary>
        /// <param name="testMasterId"></param>
        /// <returns></returns>
        IEnumerable<Test> ITestExpressionService.GetTestsByComponent(int testMasterId)
        {
            return testRepository.GetAll().Where(x => x.TestMasterID == testMasterId && x.Sequence == 1 && x.IsExecuted == true).ToList();
        }
        /// <summary>
        /// Get Equations By TestId
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        IEnumerable<Equation> ITestExpressionService.GetEquationsByTestId(int testId)
        {
            return equationRepository.GetAll().Where(x => x.TestID == testId).ToList();
        }
        /// <summary>
        /// Get Input Fields By TestId
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        IEnumerable<InputField> ITestExpressionService.GetInputFieldsByTestId(int testId)
        {
            return inputFieldRepository.GetAll().Where(x => x.TestID == testId).ToList();
        }
        /// <summary>
        /// Get Location Attributes By TestMasterId
        /// </summary>
        /// <param name="testMasterId"></param>
        /// <returns></returns>
        IEnumerable<vWLocationAttribute> ITestExpressionService.GetLocationAttributesByTestMasterId(int testMasterId)
        {
            return vWLocationAttributesRepository.GetAll().Where(x => x.TestMasterID == testMasterId).ToList();
        }
        /// <summary>
        /// Get Part Attributes By TestId
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        IEnumerable<PartAttribute> ITestExpressionService.GetPartAttributesByTestId(int testId)
        {
            return partAttributesRepository.GetAll().Where(x => x.TestID == testId).ToList();
        }
        /// <summary>
        /// Get Test Calculated Fields By TestId
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        IEnumerable<TestCalculatedField> ITestExpressionService.GetTestCalculatedFieldsByTestId(int testId)
        {
            return testCalulatedFieldsRepository.GetAll().Where(x => x.TestID == testId).ToList();
        }

        /// <summary>
        /// Update Test Expression
        /// </summary>
        /// <param name="testEntity"></param>
        /// <returns></returns>
        int ITestExpressionService.UpdateTestExpression(Test testEntity)
        {
            int retVal = 0;
            var updateTestEntity = GetTestsByTestIdAndMasterId(testEntity.TestMasterID, testEntity.TestID);

            if (testEntity != null)
            {
                if (updateTestEntity != null)
                {
                    updateTestEntity.Descriptions = testEntity.Descriptions;
                    updateTestEntity.Expression = testEntity.Expression;

                    testRepository.Update(updateTestEntity);
                    unitOfWork.Commit();
                    retVal = 1;
                }
            }

            return retVal;
        }
        /// <summary>
        /// Get Tests FromTestIdAndMasterId
        /// </summary>
        /// <param name="testMasterId"></param>
        /// <param name="testId"></param>
        /// <returns></returns>
        Test ITestExpressionService.GetTestsFromTestIdAndMasterId(int testMasterId, int testId)
        {
            return testRepository.GetAll().Where(x => x.TestMasterID == testMasterId && x.TestID == testId).FirstOrDefault();
        }
        #region "Private Methods"
        private Test GetTestsByTestIdAndMasterId(int testMasterId, int testId)
        {
            return testRepository.GetAll().Where(x => x.TestMasterID == testMasterId && x.TestID == testId).FirstOrDefault();
        }
        
        #endregion
    }
}
