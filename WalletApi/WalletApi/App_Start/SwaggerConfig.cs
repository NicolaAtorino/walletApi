using System.Web.Http;
using WebActivatorEx;
using WalletApi;
using Swashbuckle.Application;

namespace WalletApi
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            config.EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "WalletApi");
                        //c.IncludeXmlComments(GetXmlCommentsPath());
                    })
                .EnableSwaggerUi();
        }
    }
}
