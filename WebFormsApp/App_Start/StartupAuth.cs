using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace WebFormsApp
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
               new OpenIdConnectAuthenticationOptions
               {
                   // Generate the metadata address using the tenant and policy information
                   MetadataAddress = AuthConfiguration.MetadataAddress,

                   // These are standard OpenID Connect parameters, with values pulled from web.config
                   ClientId = AuthConfiguration.ClientId,
                   RedirectUri = AuthConfiguration.RedirectUri,
                   PostLogoutRedirectUri = AuthConfiguration.RedirectUri,

                   // Specify the callbacks for each type of notifications
                   Notifications = new OpenIdConnectAuthenticationNotifications
                   {
                       RedirectToIdentityProvider = OnRedirectToIdentityProvider,
                       AuthorizationCodeReceived = OnAuthorizationCodeReceived,
                       AuthenticationFailed = OnAuthenticationFailed
                   },

                   // Specify the claims to validate
                   TokenValidationParameters = new TokenValidationParameters
                   {
                       NameClaimType = "name"
                   },

                   // Specify the scope by appending all of the scopes requested into one string (separated by a blank space)
                   Scope = $"openid profile offline_access {AuthConfiguration.ReadTasksScope} {AuthConfiguration.WriteTasksScope}"
               }
           );


            // This makes any middleware defined above this line run before the Authorization rule is applied in web.config
            app.UseStageMarker(PipelineStage.Authenticate);
        }

        private Task OnRedirectToIdentityProvider(RedirectToIdentityProviderNotification<Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            var policy = notification.OwinContext.Get<string>("Policy");

            if (!string.IsNullOrEmpty(policy) && !policy.Equals(AuthConfiguration.DefaultPolicy))
            {
                notification.ProtocolMessage.Scope = OpenIdConnectScope.OpenId;
                notification.ProtocolMessage.ResponseType = OpenIdConnectResponseType.IdToken;
                notification.ProtocolMessage.IssuerAddress = notification.ProtocolMessage.IssuerAddress.ToLower().Replace(AuthConfiguration.DefaultPolicy.ToLower(), policy.ToLower());
            }

            //// Pass in any custom parameters (login hint, ui locale etc)
            //var properties = notification.OwinContext.Authentication.AuthenticationResponseChallenge?.Properties;
            //if (properties != null && properties.Dictionary.TryGetValue(AdB2CProperties.LoginHint, out var loginHint))
            //{
            //    notification.ProtocolMessage.SetParameter("login_hint", loginHint);
            //}

            return Task.FromResult(0);
        }

        private Task OnSecurityTokenValidated(SecurityTokenValidatedNotification<Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            return Task.FromResult(0);
        }

        private Task OnAuthenticationFailed(AuthenticationFailedNotification<Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            notification.HandleResponse();

            // Handle the error code that Azure AD B2C throws when trying to reset a password from the login page 
            // because password reset is not supported by a "sign-up or sign-in policy"
            if (notification.ProtocolMessage.ErrorDescription != null && notification.ProtocolMessage.ErrorDescription.Contains("AADB2C90118"))
            {
                // If the user clicked the reset password link, redirect to the reset password route
                notification.Response.Redirect("/Account/ResetPassword");
            }
            else if (notification.Exception.Message == "access_denied")
            {
                notification.Response.Redirect("/");
            }
            else
            {
                notification.Response.Redirect("/Account/Error?message=" + notification.Exception.Message);
            }

            return Task.FromResult(0);
        }


        /*
         * Callback function when an authorization code is received 
         */
        private Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedNotification notification)
        {
            // Extract the code from the response notification
            var code = notification.Code;

            //string signedInUserID = notification.AuthenticationTicket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            //TokenCache userTokenCache = new MSALSessionCache(signedInUserID, notification.OwinContext.Environment["System.Web.HttpContextBase"] as HttpContextBase).GetMsalCacheInstance();
            //ConfidentialClientApplication cca = new ConfidentialClientApplication(ClientId, Authority, RedirectUri, new ClientCredential(ClientSecret), userTokenCache, null);
            //try
            //{
            //    AuthenticationResult result = await cca.AcquireTokenByAuthorizationCodeAsync(code, Scopes);
            //}
            //catch (Exception ex)
            //{
            //    //TODO: Handle
            //    throw;
            //}

            return Task.FromResult(0);
        }

        private static string EnsureTrailingSlash(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            if (!value.EndsWith("/", StringComparison.Ordinal))
            {
                return value + "/";
            }

            return value;
        }
    }
}
