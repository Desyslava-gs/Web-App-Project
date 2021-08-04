using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Cars
{
    public class DeleteCarViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Марка")]
        public string Make { get; set; }

        [Display(Name = "Модел")]
        public string Model { get; set; }

        [Display(Name = "Година")]
        public int Year { get; set; }

        [Display(Name = "Цвят")]
        public string Color { get; set; }

        [Display(Name = "Рег.Номер")]
        public string PlateNumber { get; set; }

        [Display(Name = "Рама Номер")]
        public string VinNumber { get; set; }

        [Display(Name = "Снимка")]
        public string PictureUrl { get; set; }

        [Display(Name = "Бележка")]
        public string Description { get; set; }

        [Display(Name = "Тип Двигател")]
        public string FuelType { get; set; }
    }
}
