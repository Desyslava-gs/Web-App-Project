using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebApp.Data.DataConstants.Repair;

namespace WebApp.Models.Repairs
{
    public class EditRepairFormModel
    {
        public string Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        [Column(TypeName = PriceFormat)]
        public decimal Price { get; set; }

        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public string CarId { get; set; }

        public string RepairTypeId { get; init; }

        public IEnumerable<RepairTypeViewModel> RepairTypes { get; set; }
    }
}
