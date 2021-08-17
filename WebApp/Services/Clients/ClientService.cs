using System.Linq;
using WebApp.Data;
using WebApp.Data.Models;
using WebApp.Models.Clients;

namespace WebApp.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly CarRepairDbContext data;

        public ClientService(CarRepairDbContext data)
        {
            this.data = data;
        }

        public bool IsClient(string uid)
        {
            return this.data.Clients.Any(c => c.UserId == uid);

        }

        public void CreateClient(ClientFormModel client, string userId)
        {
            var clientData = new Client
            {
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                UserId = userId
            };

            this.data.Clients.Add(clientData);
            this.data.SaveChanges();

        }

    }
}
