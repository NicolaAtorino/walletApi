using System.Web.Http;
using WebActivatorEx;
using WalletApi;
using Swashbuckle.Application;
using System;
using System.Reflection;
using System.IO;

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

                        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
                        var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                        c.IncludeXmlComments(commentsFile);
                    })
                .EnableSwaggerUi();
        }
    }
}
