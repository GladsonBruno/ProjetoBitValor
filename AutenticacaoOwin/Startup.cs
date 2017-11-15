﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Cors;
using Microsoft.Owin.Cors;


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
            //Usando configuração personalizada do cors
            ConfigureCors(app);

            //Ativando tokens de acesso
            AtivandoAccessToken(app);

            app.UseWebApi(config);
        }

        private void AtivandoAccessToken(IAppBuilder app)
        {

        }

        //Configuração personalizada do cors
        private void ConfigureCors(IAppBuilder app)
        {
            //Definindo politica do cors
            var politica = new CorsPolicy();
            politica.AllowAnyHeader = true;
            politica.Origins.Add("http://localhost:49774/");
            politica.Origins.Add("http://localhost:49744/");
            politica.Methods.Add("GET");
            politica.Methods.Add("POST");

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(politica)
                }
            };
            app.UseCors(corsOptions);
        }
    }
}
