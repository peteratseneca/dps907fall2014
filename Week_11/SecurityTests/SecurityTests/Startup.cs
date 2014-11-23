using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using Owin;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

// Change the namespace to match the name of your project
[assembly: OwinStartup(typeof(SecurityTests.Startup))]

namespace SecurityTests
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            //Token Consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions { });
        }
    }

}
