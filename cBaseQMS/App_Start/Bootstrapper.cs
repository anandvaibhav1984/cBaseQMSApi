using Autofac;
using Autofac.Integration.Mvc;
using cBaseQMS.Areas.cBaseQMS.RestController;
using cBaseQMS.Areas.ResolveDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace cBaseQMS.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
           builder.RegisterModule(new ResolveRestClientDependency());
            //builder.RegisterModule(new ServiceModule());



            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
