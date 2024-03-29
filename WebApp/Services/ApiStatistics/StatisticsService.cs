﻿using System;
using System.Linq;
using WebApp.Data;
using WebApp.Services.ApiStatistics;

namespace WebApp.Services.Statistics
{
    public class StatisticsService : IStatisticService

    {
        private readonly CarRepairDbContext data;
        public StatisticsService(CarRepairDbContext data)
        {
            this.data = data;
        }
       
   
        public ApiStatisticsServiceModel All()
        {
            var allCars = this.data.Cars.Count();
            var finishedRepairs = this.data.Repairs.Count(r => r.EndDate < DateTime.UtcNow);
            var allRepairs = this.data.Repairs.Count();
            var allClients = this.data.Clients.Count();
              
            return new ApiStatisticsServiceModel
            {
                AllClients = allClients,
                AllCars = allCars,
                AllRepairs = allRepairs,
                FinishedRepairs = finishedRepairs,
            };
        }
    }
}
