using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.Api;
using WebApp.Services.Repairs;
using WebApp.Services.Statistics;

namespace WebApp.Controllers.Api
{
    [ApiController]
        [Route("api/statistics")]
        public class StatisticsApiController : ControllerBase
        { 
            private readonly IStatisticService statisticService;


            public StatisticsApiController(IStatisticService statisticService)
            {
                this.statisticService = statisticService;
            }

            [HttpGet]
            public ApiStatisticsServiceModel Get()
            {
                var statistics = statisticService.All();

                return statistics;
            }
        }
    }

