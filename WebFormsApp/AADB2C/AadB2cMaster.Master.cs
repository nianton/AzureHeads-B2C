using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsApp.AADB2C
{
    public partial class AadB2cMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string GetAbsoluteUrl(string relativeUrl)
        {
            var scheme = Request.IsSecureConnection ? "https" : "http";
            var hostWithPort = Request.Url.Host + (Request.Url.IsDefaultPort ? string.Empty : $":{Request.Url.Port}");
            return $"{scheme}://{hostWithPort}{ResolveUrl(relativeUrl)}";
        }
    }
}