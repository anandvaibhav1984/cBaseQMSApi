using Autofac;
using cBaseQms.DAL;
using cBaseQms.Service.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQMS.ResolveDependency
{
   public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestMasterService>().As<ITestMasterService>().InstancePerLifetimeScope();
            builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();
            builder.RegisterType<TestMasterService>().As<ITestMasterService>().InstancePerLifetimeScope();
            builder.RegisterType<TestExpressionService>().As<ITestExpressionService>().InstancePerLifetimeScope();
            builder.RegisterType<PartMasterService>().As<IPartMasterService>().InstancePerLifetimeScope();
            builder.RegisterType<LocationMasterService>().As<ILocationMasterService>().InstancePerLifetimeScope();
            builder.RegisterType<TestMasterMappingService>().As<ITestMasterMappingService>().InstancePerLifetimeScope();
            builder.RegisterType<AppParameterService>().As<IAppParameterService>().InstancePerLifetimeScope();
           
        }
    }
}
