using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(Painel_Projetos.Web.Startup))]

namespace Painel_Projetos.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Usuario/Login")
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";
        }
    }
}
