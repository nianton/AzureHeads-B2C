using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsApp.Account
{
    public partial class SignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This is the redirection end after signing out from AAD B2C
            // We can clear/abandon current user session here and redirect to home page.


            if (Request.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                Response.Redirect("~/");
            }
        }
    }
}