using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Cars;
using static WebApp.Data.DataConstants.Car;

namespace WebApp.Data.Models
{

    public class Car
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(MakeMaxLength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; set; }

        [MaxLength(ColorMaxLength)]
        public string Color { get; set; }

        [Required]
        [MaxLength(PlateNumberMaxLength)]
        public string PlateNumber { get; set; }

        [Required]
        [MaxLength(VinNumberMaxLength)]
        public string VinNumber { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        public string Description { get; set; }

       
        public string FuelTypeId { get; set; }
        public FuelType FuelType { get; init; }
        
        public IEnumerable<Repair> Repairs { get; set; } = new List<Repair>(); 
    }
}
