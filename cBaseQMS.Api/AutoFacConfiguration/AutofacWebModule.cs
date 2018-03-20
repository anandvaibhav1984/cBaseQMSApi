using Autofac;
using Autofac.Integration.WebApi;
using cBaseQMS.Api.AutoFacConfiguration;
using cBaseQMS.Api.Caching;
using cBaseQMS.Api.Validatiors;
using cBaseQMS.Service.Models;
using FluentValidation;
using FluentValidation.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Http.Validation;

namespace cBaseQMS_Api.AutoFacConfiguration
{
    public class AutofacWebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {            
            builder.RegisterType<TestMasterMappingValidator>().As<IValidator<TestMasterMappingViewModel>>().InstancePerLifetimeScope();
            builder.RegisterType<TestMasterValidator>().As<IValidator<TestMasterViewModel>>().InstancePerLifetimeScope();
            builder.RegisterType<TestValidator>().As<IValidator<TestsViewModel>>().InstancePerLifetimeScope();
            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().InstancePerLifetimeScope();
            builder.RegisterType<FluentValidationModelValidatorProvider>().As<ModelValidatorProvider>().InstancePerLifetimeScope();
            builder.RegisterType<InMemoryCache>().As<ICacheService>().InstancePerLifetimeScope();

        }
    }
}