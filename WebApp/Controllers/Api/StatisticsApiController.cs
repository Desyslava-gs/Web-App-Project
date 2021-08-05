using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.Api;

namespace WebApp.Controllers.Api
{
   
        [ApiController]
        [Route("api/statistics")]
        public class StatisticsApiController : ControllerBase
        {
            private readonly CarRepairDbContext data;

            public StatisticsApiController(CarRepairDbContext data)
                => this.data = data;

            [HttpGet]
            public StatisticsModel Get()
            {
                var statistics = new StatisticsModel
                {
                    AllCars = this.data.Cars.Count(),
                    AllClients = this.data.Clients.Count(),
                    AllRepairs = this.data.Repairs.Count(),
                    FinishedRepairs = this.data.Repairs.Count(r => r.EndDate <= DateTime.UtcNow)
                };

                return statistics;
            }


        }
    }

