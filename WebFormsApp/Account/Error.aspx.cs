using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsApp.Account
{
    public partial class Error : System.Web.UI.Page
    {
        protected string ErrorMessage => Request.QueryString["message"];

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}