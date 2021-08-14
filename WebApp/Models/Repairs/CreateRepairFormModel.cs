using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebApp.Data.DataConstants.Repair;

namespace WebApp.Models.Repairs
{
    public class CreateRepairFormModel
    {
        public string Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }
        [Required]
        [Column(TypeName = PriceFormat)]

        public decimal Price { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public string CarId { get; set; }
        [Required]
        public string RepairTypeId { get; init; }

        public IEnumerable<RepairTypeViewModel> RepairTypes { get; set; }
    }
}
