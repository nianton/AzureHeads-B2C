using GraphLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsApp.Secure
{
    public partial class UserManagement : System.Web.UI.Page
    {
        private static readonly GraphApiClient GraphClient = new GraphApiClient(AuthConfiguration.GraphClientId, AuthConfiguration.GraphClientSecret, AuthConfiguration.Tenant);
        private const string TaxRegistrationNumberPropertyName = "TaxRegistrationNumber";

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(BindUsers));
        }

        protected string GetEmailTag(User user)
        {
            var email = user.SignInNames.FirstOrDefault(sn => sn.Type == "emailAddress")?.Value;
            return string.IsNullOrEmpty(email)
                ? string.Empty
                : $"<a href='mailto:{email}'>{email}</a>";
        }

        protected string GetTaxRegistrationNumber(User user)
        {
            var taxRegistrationNumber = user.GetExtendedProperty<string>(TaxRegistrationNumberPropertyName);
            return taxRegistrationNumber;
        }

        protected void btnGenerateUser_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(CreateRandomUserAndRebind));
        }

        private async Task CreateRandomUserAndRebind()
        {
            var id = Guid.NewGuid().ToString("N").Substring(0, 6);
            var user = new User
            {
                CreationType = "LocalAccount",
                AccountEnabled = true,
                GivenName = $"John-{id}",
                Surname = $"Smith-{id}",
                DisplayName = $"Megatron{id}",
                JobTitle = "Sr. Random User",
                SignInNames = new List<SignInName>
                {
                    new SignInName()
                    {
                        Type = "emailAddress",
                        Value = $"john-{id}@smith.com"
                    }
                },
                PasswordProfile = new PasswordProfile
                {
                    EnforceChangePasswordPolicy = false,
                    ForceChangePasswordNextLogin = false,
                    Password = "123abC!!"
                }
            };

            user.SetExtendedProperty(TaxRegistrationNumberPropertyName, $"{DateTime.Now.Millisecond}{DateTime.Now.Millisecond}");

            var newUser = await GraphClient.UserCreateAsync(user);
            await BindUsers();
        }

        private async Task BindUsers()
        {
            var users = await GraphClient.UserGetListAsync();
            gvUsers.DataSource = users.Items;
            gvUsers.DataBind();
        }
    }
}