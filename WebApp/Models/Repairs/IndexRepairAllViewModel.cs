using System;

namespace WebApp.Models.Repairs
{
    public class IndexRepairAllViewModel
    {
        public string Id { get; init; } 

        public string Name { get; init; }

        public decimal Price { get; set; }
 
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }
        
        public string CarId { get; init; }

        public string CarTitle { get; set; }

        public string RepairTypeId { get; init; }

    }
}
