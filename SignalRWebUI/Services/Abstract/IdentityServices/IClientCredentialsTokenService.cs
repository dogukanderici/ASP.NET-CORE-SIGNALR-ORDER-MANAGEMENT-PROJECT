namespace SignalRWebUI.Services.Abstract.IdentityServices
{
    public interface IClientCredentialsTokenService
    {
        Task<string> GetToken();
    }
}
