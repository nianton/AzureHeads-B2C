# Docs: https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-devquickstarts-graph-dotnet#register-your-application-in-your-tenant

# Application/Id: [application_id]
# Local (to tenant) admin user: admin@azureheadsb2c.onmicrosoft.com
# Pass

Connect-MsolService # Connect with Local (to tenant) admin user 

$applicationId = "[application_id]"
$sp = Get-MsolServicePrincipal -AppPrincipalId $applicationId
Add-MsolRoleMember -RoleObjectId fe930be7-5e62-47db-91af-98c3a49a38b1 -RoleMemberObjectId $sp.ObjectId -RoleMemberType servicePrincipal