using Ninject.Web.Common.OwinHost;
using Owin;
using System.Web.Http;
using Ninject;
using System;
using System.Reflection;
using Ninject.Web.WebApi.OwinHost;

namespace WalletApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appbuilder)
        {
            var config = new HttpConfiguration();
            
            //needed for attribute routing
            config.MapHttpAttributeRoutes();

            //configuration for dependency injection specific for ninject + owin host.
            appbuilder.UseNinjectMiddleware(createKernel);
            appbuilder.UseNinjectWebApi(config);
        }

        private IKernel createKernel()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}