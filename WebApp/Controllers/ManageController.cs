using GraphLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller reserved for GraphAPI integration for AD B2C user management.
    /// </summary>
    public class ManageController : Controller
    {
        private const string TaxRegistrationNumberPropertyName = "TaxRegistrationNumber";

        private static readonly GraphApiClient GraphClient = new GraphApiClient("", "", Startup.Tenant);

        // GET: Manage
        public async Task<ActionResult> Index()
        {
            var users = await GraphClient.UserGetListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser()
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
            return Json(newUser);
        }
    }
}