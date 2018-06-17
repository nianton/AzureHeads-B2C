# AzureHeads B2C Demo

This is a Visual Studio 2017 solution to demonstrate the integration of the AAD B2C identity provider in some scenarios. The following projects are included:

* __WebApp__: it is an ASP.NET MVC 4 web application which uses B2C as the OpenIDConnect authentication mechanism, 
and it also uses MSAL.NET to acquire an access token for the logged in user in order to access a protected WebAPI/WCF service
* __WebApi__: it is an ASP.NET WebAPI 2 web project which exposes a protected REST service via OAuth bearer authentication (expects an Authorization header with a bearer token)
* __WcfServiceApp__: it is an WCF project which exposes a protected WCF service via an basichttpsbinding, with a configured implementation of a ServiceAuthorizationManager which extracts and explicitly validates the bearer token from the Authorization header.
There is a second part to make the service client of this WCF service work, which resides on the __WebApp__ project where on the controller __TaskServiceCotroller__ the instantiated WCF client is configured with an AuthorizationHeaderEndpointBehavior which injects the bearer token on the outgoing message.
* __WebFormsApp__: it is an ASP.NET Web Forms project which used B2C as the OpenIDConnect authentication mechanism, with a sample implementation of how to indicate user statu


