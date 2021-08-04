using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static WebApp.Data.DataConstants.Repair;

namespace WebApp.Data.Models
{
    public class Repair
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        [Column(TypeName = PriceFormat)]
        public decimal Price { get; set; }
       //?????
        public DateTime? StartDate { get; set; }
       //??????
        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        [Required]                                     //?
        public string CarId { get; init; }

        public Car Car { get; init; }

        [Required]                                      //?
        public string RepairTypeId { get; init; }

        public RepairType RepairType { get; init; }

        public IEnumerable<Part> Parts { get; set; } = new List<Part>();

    }
}
