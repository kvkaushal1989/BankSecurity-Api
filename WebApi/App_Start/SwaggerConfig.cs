using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using System;
using System.Xml.XPath;
using System.IO;
using System.Collections.Generic;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Linq;
using WebApi;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApi
{
    public class SwaggerConfig
    {
        public static void Register() {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c => {                       
                    c.SingleApiVersion("v1", "WebApi");

                    c.OAuth2("oauth2")
                        .Description("client credentials grant flow")
                        .Flow("password")
                        .Scopes(scopes => { scopes.Add("read", "Read access to protected resources"); })
                        //  .TokenUrl(Path.Combine(System.Configuration.ConfigurationManager.AppSettings["Url"].ToString(), "/token"));
                        .TokenUrl(System.Configuration.ConfigurationManager.AppSettings["Url"].ToString());
                    //.TokenUrl("http://localhost:18162/token");
                    c.OperationFilter<AssignOAuth2SecurityRequirements>();
                 })
                .EnableSwaggerUi(c => {
                    c.EnableOAuth2Support("HkWebApiapi", "WebApirealm", "WebApi");
                });
        }

        protected static string GetXmlCommentsPath() {
            return Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "App_Data", "XMLDocumentation.xml");
        }
    }


    public class AssignOAuth2SecurityRequirements : IOperationFilter {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription) {
            var actFilters = apiDescription.ActionDescriptor.GetFilterPipeline();
            var allowsAnonymous = actFilters.Select(f => f.Instance).OfType<OverrideAuthorizationAttribute>().Any();
            if (allowsAnonymous)
                return; // must be an anonymous method


            //var scopes = apiDescription.ActionDescriptor.GetFilterPipeline()
            //    .Select(filterInfo => filterInfo.Instance)
            //    .OfType<AllowAnonymousAttribute>()
            //    .SelectMany(attr => attr.Roles.Split(','))
            //    .Distinct();

            if (operation.security == null)
                operation.security = new List<IDictionary<string, IEnumerable<string>>>();

            var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
        {
            {"oauth2", new List<string> {"WebApi"}}
        };

            operation.security.Add(oAuthRequirements);
        }
    }
}
