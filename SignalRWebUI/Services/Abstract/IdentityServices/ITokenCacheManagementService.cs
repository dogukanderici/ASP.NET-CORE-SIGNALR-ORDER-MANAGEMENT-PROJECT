namespace SignalRWebUI.Services.Abstract.IdentityServices
{
    public interface ITokenCacheManagementService
    {
        void SetToken(string clientName, string token, int expires);
        string GetToken(string clientName);
        bool RemoveToken(string clientName);
    }
}
