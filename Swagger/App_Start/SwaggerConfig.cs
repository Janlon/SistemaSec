using System.Web.Http;
using WebActivatorEx;
using Swagger;
using Swashbuckle.Application;
using System;
using System.Linq;
using System.IO;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Swagger
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            var caminho = $@"{AppDomain.CurrentDomain.BaseDirectory}bin\Swagger.xml";

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Sistema Sec");
                    c.IgnoreObsoleteActions();
                    c.UseFullTypeNameInSchemaIds();
                    c.IncludeXmlComments(caminho);
                    c.IgnoreObsoleteProperties();
                    c.DescribeAllEnumsAsStrings();
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
