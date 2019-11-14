using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
[assembly:OwinStartup(typeof(FoodPetWebApi.Startup))]
namespace FoodPetWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {

                AuthenticationType ="ApplicationCookie", //string que identifica o cookie
                LoginPath = new PathString("/autenticacao/login")
            });
        }
    }
}