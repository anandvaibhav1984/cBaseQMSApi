using Autofac;
using cBaseQms.Repository.Infrastructure;
using cBaseQms.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQMS.ResolveDependency
{
    public class RepositryModule :Module
    {
    protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TestMasterRepository>().As<ITestMasterRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TestRepositry>().As<ITestRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TestExpressionRepository>().As<ITestExpressionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PartMasterRepositry>().As<IPartMasterRepositry>().InstancePerLifetimeScope();
            builder.RegisterType<LocationMasterRepositry>().As<ILocationMasterRepositry>().InstancePerLifetimeScope();
            builder.RegisterType<TestMasterMappingRepositry>().As<ITestMasterMappingRepositry>().InstancePerLifetimeScope();
            builder.RegisterType<ComponentRepositry>().As<IComponentRepositry>().InstancePerLifetimeScope();
            builder.RegisterType<AppParameterRepository>().As<IAppParameterRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquationRepositry>().As<IEquationRepositry>().InstancePerLifetimeScope();
            builder.RegisterType<TestInputFieldsRepositry>().As<ITestInputFieldsRepositry>().InstancePerLifetimeScope();
            builder.RegisterType<VWLocationAttributesRepository>().As<IVWLocationAttributesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PartAttributesRepository>().As<IPartAttributesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TestCalculatedFieldsRepository>().As<ITestCalculatedFieldsRepository>().InstancePerLifetimeScope();
        }
    }
}
