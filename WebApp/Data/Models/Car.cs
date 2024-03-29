﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string ClientId { get; set; }

        public Client Client { get; init; }

        public IEnumerable<Repair> Repairs { get; set; } = new List<Repair>();
    }
}
