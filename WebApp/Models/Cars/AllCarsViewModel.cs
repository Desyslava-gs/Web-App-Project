using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Cars
{
    public class AllCarsViewModel
    {
        public string SearchList { get; set; }
        public List<IndexCarAllViewModel> CarsList { get; set; }
    }
}
