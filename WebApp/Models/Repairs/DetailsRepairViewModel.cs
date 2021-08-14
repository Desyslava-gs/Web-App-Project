using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Data.Models;

namespace WebApp.Models.Repairs
{
    public class DetailsRepairViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Ремонт")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Начало")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Край")]
        public DateTime EndDate { get; set; }
        
        [Display(Name = "Снимка")]
        public string PictureUrl { get; set; }

        [Display(Name = "Бележка")]

        public string Description { get; set; }
        [Display(Name = "Кола")]

        public string CarTitle { get; set; }
         
        [Display(Name = "Автомобил")]
        public string CarId { get; init; }
       
        [Display(Name = "Тип Ремонт")]

        public string RepairTypeId { get; init; }

        [Display(Name = "Части")]
        public  string PartName { get; set; }
        public IEnumerable<Part> Parts { get; set; }
    }
}
