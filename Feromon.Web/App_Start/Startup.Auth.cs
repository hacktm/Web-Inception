using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using Feromon.Web.Models;

namespace Feromon.Web
{
    public partial class Startup
    {
        const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");


            // Facebook :
            // https://developers.facebook.com/apps
            var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions
                                  {
                                      AppId = "553580641439024",
                                      AppSecret = "0a2fbd928beabe91429776f9f68b2d20",
                                      Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider
                                                 {
                                                     OnAuthenticated = (context) =>
                                                                       {
                                                                           context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
                                                                           foreach (var x in context.User)
                                                                           {
                                                                               var claimType = string.Format("urn:facebook:{0}", x.Key);
                                                                               var claimValue = x.Value.ToString();
                                                                               if (!context.Identity.HasClaim(claimType, claimValue))
                                                                                   context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, XmlSchemaString, "Facebook"));

                                                                           }
                                                                           return Task.FromResult(0);
                                                                       }
                                                 }
                                  };
            facebookOptions.Scope.Add("email,public_profile,user_friends,user_videos");
            app.UseFacebookAuthentication(facebookOptions);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});


        }
    }
}