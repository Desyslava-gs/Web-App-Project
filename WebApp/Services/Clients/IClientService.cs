using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Services.Clients
{
    public interface IClientService
    {
        public bool IsClient(string uid);
    }
}
