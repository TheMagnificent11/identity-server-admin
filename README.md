# identity-server-admin

The aim of this project is to provide administrative features for IdentityServer (https://www.identityserver.com/).

While IdentityServer is free, the administrative client costs several hundred dollars per year for the "starter" package (https://www.identityserver.com/products/).


## Getting started

1. Clone this repository
2. Using a command console in the root directory of the solution, run the following commands
   2.1 `dotnet restore`
   2.2 `dotnet build --no-restore`
3. Navigate to `IdentityServer` directory in a command console and run the following commands
   3.1 `dotnet user-secrets set ConnectionStrings:DefaultConnection "[connection string to MS SQL Server database]"`
   3.2 `dotnet user-secrets set AdminClient:ApiName [Admin API Client Name]`
   3.3 `dotnet user-secrets set AdminClient:ClientId [Admin API Client ID]`
   3.4 `dotnet user-secrets set AdminClient:ClientSecret [Admin API Client Secret]`
   3.5 `dotnet run`
3. Navigate to `IdentityServer.Admin` directory in a second command console and run the following commands
   3.1 `dotnet user-secrets set ConnectionStrings:DefaultConnection "[same value as Step 3.1]"`
   3.2 `dotnet user-secrets set AuthServer:Audience [same value as step 3.2]`
   3.3 `dotnet run`
4. TODO: postman
