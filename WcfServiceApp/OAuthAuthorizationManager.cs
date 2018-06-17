using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace WcfServiceApp
{
    /// <summary>
    /// 
    /// </summary>
    public class OAuthAuthorizationManager : ServiceAuthorizationManager
    {
        private const string HttpRequestPropertyName = "httpRequest";
        private const string AuthorizationHeaderName = "Authorization";
        private const string BearerTokenPrefix = "Bearer";
        private const string AuthContextPrincipalPropertyName = "Principal";

        private readonly ConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

        public OAuthAuthorizationManager()
        {
            // Build B2C configuration manager
            var b2cEndpoint = string.Format(AuthSettings.AadInstance, AuthSettings.Tenant, AuthSettings.DefaultPolicy);
            _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(b2cEndpoint, new OpenIdConnectConfigurationRetriever());
        }

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            // Extract the action URI from the OperationContext. Match this against the claims 
            // in the AuthorizationContext. 
            string action = operationContext.RequestContext.RequestMessage.Headers.Action;

            try
            {
                // Get the message
                var message = operationContext.RequestContext.RequestMessage;

                // Retrieve Http headers from the message
                var httpHeaders = ((HttpRequestMessageProperty)message.Properties[HttpRequestPropertyName]).Headers;

                // Get authorization header token value
                var authHeader = httpHeaders.GetValues(AuthorizationHeaderName)?.SingleOrDefault();
                if (authHeader != null)
                {
                    var parts = authHeader.Split(' ');
                    if (parts.Length == 2 && string.Equals(parts[0], BearerTokenPrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        var jwtToken = parts[1];

                        // Validate JWT token and get the user principal
                        var userPrincipal = ValidateJwtAsync(jwtToken).Result;
                        if (userPrincipal != null)
                        {
                            // Injecting the principal in the operation context to be available as Thread.CurrentPrincipal in the service instance
                            operationContext.ServiceSecurityContext.AuthorizationContext.Properties[AuthContextPrincipalPropertyName] = userPrincipal;
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private async Task<TokenValidationParameters> GetValidationParametersAsync()
        {
            var config = await _configurationManager.GetConfigurationAsync();

            return new TokenValidationParameters()
            {
                ValidAudience = AuthSettings.ClientId,
                IssuerSigningKeys = config.SigningKeys.OfType<RsaSecurityKey>(),
                ValidIssuer = config.Issuer,
                AuthenticationType = AuthSettings.SignUpSignInPolicy,
                RequireExpirationTime = true
            };
        }

        /// <summary>
        /// Validates the JWT token and returns back the user's <see cref="ClaimsPrincipal"/> instance, if valid.
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        private async Task<ClaimsPrincipal> ValidateJwtAsync(string jwt)
        {
            // TODO: Remove in production, helps with exception messages during debugging
            IdentityModelEventSource.ShowPII = true;

            // Read Jwt token and create validation parameters based on issuer
            var handler = new JwtSecurityTokenHandler();

            var validationParameters = await GetValidationParametersAsync();
            if (validationParameters == null)
            {
                return null;
            }

            try
            {
                var principal = handler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}