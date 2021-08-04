using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public string StartDate { get; set; }


        [Display(Name = "Край")]
        public string EndDate { get; set; }
        
        [Display(Name = "Снимка")]
        public string PictureUrl { get; set; }

        [Display(Name = "Бележка")]
        public string Description { get; set; }
        [Display(Name = "Кола")]
        public string CarTitle { get; set; }
         
        //?
        [Display(Name = "?")]
        public string CarId { get; init; }
       
        [Display(Name = "/")]
        public string RepairTypeId { get; init; }


    }
}
