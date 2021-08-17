using WebApp.Models.Clients;

namespace WebApp.Services.Clients
{
    public interface IClientService
    {
        public bool IsClient(string uid);

        public void CreateClient(ClientFormModel client, string userId);
    }
}
