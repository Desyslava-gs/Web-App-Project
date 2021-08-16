using System.Collections.Generic;
using WebApp.Models.Api.Cars;

namespace WebApp.Services.Cars
{
   public interface ICarService
    {
        public IList<ApiCarsModel> All();
    }
}
