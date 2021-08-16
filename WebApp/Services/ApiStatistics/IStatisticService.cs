using System.Collections.Generic;
using WebApp.Models.Api.Cars;

namespace WebApp.Services.Statistics
{
    public interface IStatisticService
    {
        ApiStatisticsServiceModel All();
    }
}
