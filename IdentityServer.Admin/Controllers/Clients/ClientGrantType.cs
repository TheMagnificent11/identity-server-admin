namespace IdentityServer.Admin.Controllers.Clients
{
    public enum ClientGrantType
    {
        ClientCredentials,
        ResourceOwnerPassword,
        AuthorizationCode,
        Implicit,
        Hybrid,
        DeviceFlow
    }
}
