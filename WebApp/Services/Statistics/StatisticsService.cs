using System;
using System.Linq;
using WebApp.Data;

namespace WebApp.Services.Statistics
{
    public class StatisticsService : IStatisticService

    {
        private readonly CarRepairDbContext data;
        public StatisticsService(CarRepairDbContext data)
        {
            this.data = data;
        }

        public StatisticsServiceModel All()
        {
            var allCars = this.data.Cars.Count();
            var finishedRepairs = this.data.Repairs.Count(r => r.EndDate < DateTime.UtcNow);
            var allRepairs = this.data.Repairs.Count();
            var allClients = this.data.Clients.Count();
              
            return new StatisticsServiceModel
            {
                AllClients = allClients,
                AllCars = allCars,
                AllRepairs = allRepairs,
                FinishedRepairs = finishedRepairs,
            };
        }
    }
}
