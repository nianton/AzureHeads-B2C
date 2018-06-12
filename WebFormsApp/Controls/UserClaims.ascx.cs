using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsApp.Controls
{
    public partial class UserClaims : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                gvUserClaims.DataSource = ((ClaimsPrincipal)Page.User).Claims.OrderBy(c => c.Type);
                gvUserClaims.DataBind();
            }
        }       
    }
}