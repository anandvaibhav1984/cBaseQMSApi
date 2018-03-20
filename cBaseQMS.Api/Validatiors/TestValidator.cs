using cBaseQms.Repository.Repositories;
using cBaseQMS.Service.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBaseQMS.Api.Validatiors
{
    public class TestValidator : AbstractValidator<TestsViewModel>
    {
        public TestValidator(ITestRepository testsRepositry)
        {
            //RuleFor(x => x.TestName).NotEmpty().When(validateOperation).WithMessage("test Name Can't be empty").
            //   // Must(o=>false)
            //   Must((o, x) => { return testsRepositry.ifExistsTestName(o.TestName, o.TestMasterID); })
            //    //.When(o => true)
            //    .When(validateOperation)
            //    .WithMessage("Test Name already exits");


            When(validateOperation, () =>
            {
                RuleFor(x => x.TestName).NotEmpty().WithMessage("test Name Can't be empty");
                RuleFor(x => x.TestName).Must((o, x) => { return testsRepositry.ifExistsTestName(o.TestName, o.TestMasterID); }).WithMessage("Test Name already exists");
            });

            

        }
        private bool validateOperation(TestsViewModel testsViewModel)
        {
            if (testsViewModel.Operation.ToLower() != "delete")// || testsViewModel.Operation.ToLower() == "update" || testsViewModel.Operation.ToLower() == "move" || testsViewModel.Operation.ToLower() == "update"
            {
                return true;
            }
            else
                return false;
        }

        
    }
}