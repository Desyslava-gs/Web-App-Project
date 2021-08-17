using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services.ApiCars
{
  public interface ICarApiService
  {
      public IList<ApiCarsModel> All();
  }
}
