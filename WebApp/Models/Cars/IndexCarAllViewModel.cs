using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Cars
{
    public class IndexCarAllViewModel 
    {
        public string Id { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }

        public string PlateNumber { get; set; }

        public int Year { get; set; }

        public string PictureUrl { get; set; }
        
    }
}
