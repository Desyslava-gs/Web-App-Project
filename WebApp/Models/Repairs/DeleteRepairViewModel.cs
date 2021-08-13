using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Repairs
{
    public class DeleteRepairViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Ремонт")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Начало")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public string StartDate { get; set; }

        [Display(Name = "Край")]
        public string EndDate { get; set; }
        

        [Display(Name = "Бележка")]

        public string Description { get; set; }

        [Display(Name = "Автомобил")]
        public string CarId { get; init; }
        
    }
}
