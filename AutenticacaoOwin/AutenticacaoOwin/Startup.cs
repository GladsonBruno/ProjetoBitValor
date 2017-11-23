using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using System.Web.Cors;
using Microsoft.Owin.Security.OAuth;

//[assembly: OwinStartup(typeof(AutenticacaoOwin.Startup))]

namespace AutenticacaoOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );
            //Ativando o cors
            ConfigureCors(app);

            //Ativando tokens de acesso
            AtivandoAccessTokens(app);

            //Ativando a configuração da web api
            app.UseWebApi(config);
        }

        private void AtivandoAccessTokens(IAppBuilder app)
        {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                //Permite acessar o endereço de fornecimento de tokens sem Https
                AllowInsecureHttp = true,
                //Configuração de endereço de token de acesso
                TokenEndpointPath = new PathString("/token"),
                //Configurando validade de um token de acesso já fornecido
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                Provider = new ProviderDeTokensDeAcesso()
            };
            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        //Configurando os acesso do cors
        private void ConfigureCors(IAppBuilder app)
        {
            var politica = new CorsPolicy();
            politica.AllowAnyHeader = true;
            //politica.Origins.Add("http://localhost:49774/");
            //politica.Origins.Add("http://localhost:49744/");
            politica.AllowAnyOrigin = true;
            politica.Methods.Add("GET");
            politica.Methods.Add("POST");

            var CorsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(politica)
                }
            };
            app.UseCors(CorsOptions);
        }
    }
}
