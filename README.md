# identity-server-admin

The aim of this project is to provide administrative features for IdentityServer (<https://www.identityserver.com/).>

While IdentityServer is free, the administrative client costs several hundred dollars per year for the "starter" package (<https://www.identityserver.com/products/).>

## Build Status

[![Build status](https://saji.visualstudio.com/Open%20Source/_apis/build/status/Identity%20Server%20Admin)](https://saji.visualstudio.com/Open%20Source/_build/latest?definitionId=34)

## Soltuion Architecture

The `IdentityServer` ASP.Net Core website is a standard Identity Server configured using the step in this tutorial: <http://docs.identityserver.io/en/latest/quickstarts/7_entity_framework.html.>  It has an MS SQL Server database, and connects to it via Entity Framework Core.

The `IdentityServer.Admin` ASP.Net Core website is an API website that has been configured as a client of the `IdentityServer`.  However, it share the same data access layer and can use the same database contexts to read and write data.

The idea is that other client sites can obtain a token from `IdentityServer` and use that token to call `IdentityServer.Admin` to manage users, clients and their associated claims.

## Getting started

1. Clone this repository
2. Using a command console in the root directory of the solution, run the following commands
   * `dotnet restore`
   * `dotnet build --no-restore`
3. Navigate to `IdentityServer` directory in a command console and run the following commands
   * `dotnet user-secrets set ConnectionStrings:DefaultConnection "[connection string to MS SQL Server database]"`
   * `dotnet user-secrets set AdminClient:ApiName [Admin API Client Name]`
   * `dotnet user-secrets set AdminClient:ClientId [Admin API Client ID]`
   * `dotnet user-secrets set AdminClient:ClientSecret [Admin API Client Secret]`
   * `dotnet run`
4. Navigate to `IdentityServer.Admin` directory in a second command console and run the following commands
   * `dotnet user-secrets set ConnectionStrings:DefaultConnection "[same value as Step 3.1]"`
   * `dotnet user-secrets set AuthServer:Audience [same value as step 3.2]`
   * `dotnet run`
5. Use Postman to request token and all admin API
   * Import the Postman collection (`postman_collection.json` in root of this solution)
   * Set values for the `client_id`, `client_secret` and `audience` environment/collection variables based on values setup in Dotnet User Secrets
   * Execute `Token` Postman request
     * This will request a token from `IdentityServer` website setup in step 3) and store the result in a `token` Postman variable to be used in the next request
   * Execute the `Create User` Postman
     * This is supposed to create a new user using the `IdentityServer.Admin` website setup in step 4 (currently broken and getting a 404)
