using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Configuration;
using System.Web.Http;
using System.Linq;
using Magpie.API.AuthenticationFilters;

namespace Magpie.API
{
    public class Startup
    {
        private readonly string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
        private readonly string audienceSecretConfig = ConfigurationManager.AppSettings["as:AudienceSecret"];
        private readonly string issuer = "http://altiussystems.com";

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenConsumption(app);

            ConfigureWebApi(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);

            if (string.IsNullOrWhiteSpace(audienceId))
            {
                httpConfig.Filters.Add(new CustomAuthenticationFilterAttribute());
            }
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(audienceSecretConfig);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    },
                });
        }
        
        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<System.Net.Http.Formatting.JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}