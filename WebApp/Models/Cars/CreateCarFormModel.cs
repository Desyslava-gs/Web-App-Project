﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static WebApp.Data.DataConstants.Car;

namespace WebApp.Models.Cars
{
    public class CreateCarFormModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(MakeMaxLength, MinimumLength = MakeMinLength)]
        public string Make { get; set; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength =ModelMinLength)]
        public string Model { get; set; }

        [Range(YearMinValue,YearMaxValue)]
        public int Year { get; set; }
     
        [Required]
        [StringLength(ColorMaxLength, MinimumLength =ColorMinLength)]
        public string Color { get; set; }

        [Required]
        [MaxLength(PlateNumberMaxLength)]
        [RegularExpression("^[A-Z]{2}[0-9]{4}[A-Z]{2}$")]
        public string PlateNumber { get; set; }

        [Required]
        [StringLength(VinNumberMaxLength, MinimumLength =VinNumberMinLength)]
        public string VinNumber { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }
     
        [Required]
        [StringLength(
            int.MaxValue, 
            MinimumLength = DescriptionMinLength, 
            ErrorMessage = "The field Description must be minimum length of {2}.")]
        public string Description { get; set; }
        
        public string ClienId { get; set; }
        public string FuelTypeId { get; set; }
        public IEnumerable<FuelTypeViewModel> FuelTypes { get; set; }
    }
}