using System.Configuration;

namespace WebFormsApp
{
    public sealed class AuthConfiguration
    {
        static AuthConfiguration()
        {
            AadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
            Tenant = ConfigurationManager.AppSettings["ida:Tenant"];
            ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
            SignUpSignInPolicy = ConfigurationManager.AppSettings["ida:SignUpSignInPolicyId"];
            EditProfilePolicy = ConfigurationManager.AppSettings["ida:EditProfilePolicyId"];
            ResetPasswordPolicy = ConfigurationManager.AppSettings["ida:ResetPasswordPolicyId"];
            RopcPolicy = ConfigurationManager.AppSettings["ida:RopcPolicyId"];
            RedirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
            ServiceUrl = ConfigurationManager.AppSettings["api:TaskServiceUrl"];
            ApiIdentifier = ConfigurationManager.AppSettings["api:ApiIdentifier"];
            ReadTasksScope = ApiIdentifier + ConfigurationManager.AppSettings["api:ReadScope"];
            WriteTasksScope = ApiIdentifier + ConfigurationManager.AppSettings["api:WriteScope"];

            GraphClientId = ConfigurationManager.AppSettings["aad:GraphClientId"];
            GraphClientSecret = ConfigurationManager.AppSettings["aad:GraphClientSecret"];
        }

        public static string AadInstance { get; }
        public static string Tenant { get; }
        public static string ClientId { get; }
        public static string RedirectUri { get; }
        public static string ServiceUrl { get; }
        public static string ApiIdentifier { get; }
        public static string ReadTasksScope { get; }
        public static string WriteTasksScope { get; }
        public static string[] Scopes => new string[] { ReadTasksScope, WriteTasksScope };

        public static string SignUpSignInPolicy { get; }
        public static string EditProfilePolicy { get; }
        public static string ResetPasswordPolicy { get; }
        public static string RopcPolicy { get; }
        public static string DefaultPolicy => SignUpSignInPolicy;
        public static string MetadataAddress => string.Format(AadInstance, Tenant, DefaultPolicy);
        public static string RopcTokenEndpoint => $"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/token?p={RopcPolicy}";

        public static string GraphClientId { get; }
        public static string GraphClientSecret { get; }
    }
}