using System.Web.Http;
using WebActivatorEx;
using ApiSwagger;
using Swashbuckle.Application;
using System;
using System.Linq;
using System.Net.Http;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ApiSwagger
{

    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Sistema Sec");
                    c.IgnoreObsoleteActions();
                    c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\bin\ApiSwagger.xml");
                    c.IgnoreObsoleteProperties();
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("API de Comunicação do Sistema Sec");
                    c.DocExpansion(DocExpansion.List);
                });
        }
    }
}
