using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.UI;
using WebFormsApp.Helpers;

namespace WebFormsApp.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected AuthResult AuthResult { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ApplyPageRenderState();
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(AuthenticateAsync));
        }

        private async Task AuthenticateAsync()
        {
            string username = tbUserName.Text;
            string password = tbPassword.Text;

            IAuthService authenticationService = new AuthService(AuthConfiguration.RopcTokenEndpoint, AuthConfiguration.ClientId);
            AuthResult authenticationResult = await authenticationService.AuthorizeAsync(username, password, AuthConfiguration.Scopes);
            AuthResult = authenticationResult;
            ApplyPageRenderState();
        }

        private void ApplyPageRenderState()
        {
            bool hasAuthResult = AuthResult != null;

            pErrorCode.InnerText = AuthResult?.Error?.Error;
            tbErrorDescription.Text = AuthResult?.Error?.Description;
            tbAccessToken.Text = AuthResult?.TokenResponse?.AccessToken;
            pAuthResponseJson.InnerText = hasAuthResult ? JsonConvert.SerializeObject(AuthResult.TokenResponse) : string.Empty;

            phLogin.Visible = !hasAuthResult;
            phAuthInfo.Visible = hasAuthResult && AuthResult.IsSuccessful;
            phErrorInfo.Visible = hasAuthResult && !AuthResult.IsSuccessful;
            tbUserName.Enabled = !hasAuthResult;
        }
    }
}