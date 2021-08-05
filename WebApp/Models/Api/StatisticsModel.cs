using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Api
{
    public class StatisticsModel
    {
        public int AllCars { get; set; }
        public int AllRepairs { get; set; }
        public int FinishedRepairs { get; set; }
        public int AllClients { get; set; }
    }
}
