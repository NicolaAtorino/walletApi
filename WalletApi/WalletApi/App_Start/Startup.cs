using Owin;
using System.Web.Http;

namespace WalletApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appbuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            appbuilder.UseWebApi(config);
        }
    }
}