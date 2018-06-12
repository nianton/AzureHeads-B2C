using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsApp.Account
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Let the middleware know you are trying to use the reset password policy (see OnRedirectToIdentityProvider in Startup.Auth.cs)
                Context.GetOwinContext().Set("Policy", AuthConfiguration.ResetPasswordPolicy);

                // Set the page to redirect to after changing passwords
                var authenticationProperties = new AuthenticationProperties { RedirectUri = "/" };
                Context.GetOwinContext().Authentication.Challenge(authenticationProperties);
            }
        }
    }
}