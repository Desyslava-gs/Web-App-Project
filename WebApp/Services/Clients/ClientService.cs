using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

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
    }
}
