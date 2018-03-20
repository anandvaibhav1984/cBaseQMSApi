using Autofac;
using cBaseQms.Service.AutoMapperConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using cBaseQms.Service;

namespace cBaseQMS.ResolveDependency
{
    public class AutoMapperModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AutoMapperModule).Assembly).As<Profile>();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
                

                //foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                //{
                //    cfg.AddProfile(profile);
                //}
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
