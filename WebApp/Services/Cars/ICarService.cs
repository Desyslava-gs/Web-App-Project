using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Cars;

namespace WebApp.Services.Cars
{
    public interface ICarService
    {
        public DetailsCarViewModel DetailsCar(string id);
    } 
}
