using Autofac;
using cBaseQMS.Areas.cBaseQMS.RestController;
using cBaseQMS.Areas.RestController;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBaseQMS.Areas.ResolveDependency
{
    public class ResolveRestClientDependency:Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<TestMasterClient>().As<ITestMasterClient>().InstancePerRequest();
            builder.RegisterType<RestClientBase>().As<IRestClientBase>().InstancePerRequest();
            //builder.RegisterType<RestClient>().As<IRestClient>().WithParameter("baseUrl",Constants.ApiUrl);
            //builder.Register((c, p) => new RestClient(Constants.ApiUrl));
            builder.RegisterType<TestClient>().As<ITestClientService>().InstancePerRequest();

        }
    }
}