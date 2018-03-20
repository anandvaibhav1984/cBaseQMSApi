using cBaseQms.Repository.Repositories;

using cBaseQMS.Service.Models;
using FluentValidation;

namespace cBaseQMS.Api.Validatiors
{
    public class TestMasterValidator : AbstractValidator<TestMasterViewModel>
    {
        public TestMasterValidator(ITestMasterRepository testMasterRepository)
        {
            // RuleFor(p => p.TestMasterName).NotEmpty().WithMessage("test master name can't be empty");
            RuleFor(p => p.TestMasterName).NotEmpty().When(p => p.operation.ToLower() != "delete").WithMessage("test master name can't be empty"); ;
            //RuleFor(p => p.TestMasterName).NotEmpty().Must((o, x) => { return validateOpertaion(o.operation);})
            //.WithMessage("test master name can't be empty");
            RuleFor(p => p.TestMasterName).Must(testMasterRepository.GetCountTestMasterByName)
                .When(p => p.operation.ToLower() != "delete").WithMessage("Another TestMasterName with same name exists");
            // RuleFor(p => p.Description).NotEmpty().WithMessage("Description can't be empty");
            // RuleFor(d => d.operation).Must(validateOpertaion).DependentRules(x =>{
            //   x.RuleFor(p => p.TestMasterName).Must(testMasterRepository.GetCountTestMasterByName).WithMessage("Another TestMasterName with same name exists");
            //});
        }

        private bool validateOpertaion(string operation)
        {
            if (operation != "Delete")
                return true;
            else
                return false;
        }
    }
}