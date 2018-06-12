using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace WebFormsApp.Controls
{
    public partial class UserInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lsUserStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            // Redirect to ~/Account/SignOut after signing out.
            string callbackUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Response.ApplyAppPathModifier("~/Account/SignOut");

            Context.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectAuthenticationDefaults.AuthenticationType,
                CookieAuthenticationDefaults.AuthenticationType);
        }

        protected void lbSigninIn_Click(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Context.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        protected void lbChangePassword_Click(object sender, EventArgs e)
        {
            // Let the middleware know you are trying to use the reset password policy (see OnRedirectToIdentityProvider in Startup.Auth.cs)
            Context.GetOwinContext().Set("Policy", AuthConfiguration.ResetPasswordPolicy);

            // Set the page to redirect to after changing passwords
            var authenticationProperties = new AuthenticationProperties { RedirectUri = "/" };
            Context.GetOwinContext().Authentication.Challenge(authenticationProperties);
        }

        protected void lbEditProfile_Click(object sender, EventArgs e)
        {
            // Let the middleware know you are trying to use the edit profile policy (see OnRedirectToIdentityProvider in Startup.Auth.cs)
            Context.GetOwinContext().Set("Policy", AuthConfiguration.EditProfilePolicy);

            // Set the page to redirect to after editing the profile
            var authenticationProperties = new AuthenticationProperties { RedirectUri = "/" };
            Context.GetOwinContext().Authentication.Challenge(authenticationProperties);
        }
    }
}