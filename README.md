# AzureHeads B2C Demo

This is a Visual Studio 2017 solution to demonstrate the integration of the AAD B2C identity provider in some scenarios. It was presented on the AzureHeads' event on the 14th of June 2018.

The following projects are included:

1. [Web App](#webapp)
2. [Web Api](#webapi)
3. [Wcf Service](#wcfservice)
4. [Web Forms App](#webformsapp)
5. [User Migration Console App](#usermigrationapp)

## Web App [](#){name=webapp}
It is an ASP.NET MVC 4 web application which uses B2C as the OpenIDConnect authentication mechanism, 
and it also uses MSAL.NET to acquire an access token for the logged in user in order to access a protected WebAPI/WCF service

## Web Api [](#){name=webapi}
Tt is an ASP.NET WebAPI 2 web project which exposes a protected REST service via OAuth bearer authentication (expects an Authorization header with a bearer token)

## WcfServiceApp [](#){name=wcfservice}
it is an WCF project which exposes a protected WCF service via an basichttpsbinding, with a configured implementation of a ServiceAuthorizationManager which extracts and explicitly validates the bearer token from the Authorization header.
There is a second part to make the service client of this WCF service work, which resides on the __WebApp__ project where on the controller __TaskServiceCotroller__ the instantiated WCF client is configured with an AuthorizationHeaderEndpointBehavior which injects the bearer token on the outgoing message.

## WebFormsApp [](#){name=webformsapp}
It is an ASP.NET Web Forms project which used B2C as the OpenIDConnect authentication mechanism, with a sample implementation of how to indicate user status.

## User Migration App [](#){name=usermigrationapp}
It is an .NET Console Application which demonstrates the use of Graph API (by using the __[GraphLite](https://www.nuget.org/packages/GraphLite/)__ nuget package) in order to migrate local users to AAD B2C. As the local source of users it relies on Microsoft's [WorldWideImporters](https://cloudblogs.microsoft.com/sqlserver/2016/06/09/wideworldimporters-the-new-sql-server-sample-database/) sample database.

Apply your tenant's values on the App.config. The values needed are the application's Id and secret that was created on the [Azure Active Directory -> App Registrations] blade, NOT on [Azure AD B2C] blade.
<br>__NOTE__: The application has to be granted permission "Read and write directory data" in order to have access create/update users etc.
<br>__NOTE #2__: In order for the application be granted permission to delete Users, the powershell script included in the solution AzureHeadsB2c-GrantDelete.ps1
        has to run using a local (to the tenant) admin user.