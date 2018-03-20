
using cBaseQms.Repository.Repositories;

using cBaseQMS.Service.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBaseQMS.Api.Validatiors
{
    public class TestMasterMappingValidator : AbstractValidator<TestMasterMappingViewModel>
    {
        public TestMasterMappingValidator(ITestMasterMappingRepositry testMasterMappingRepositry)
        {

           
            RuleFor(x => x.LocationMasterID).Must((o, t) => {
                return testMasterMappingRepositry.IfExistsPartAndLocationCombination(o.LocationMasterID, o.PartMasterID, o.TestMasterID);
            }) .WithMessage("combination of part and location exists");

        }

        bool validatePartAndMaster(int val)
        {
            if (val == 0)
                return false;
            else return true;
        }
    }
}
