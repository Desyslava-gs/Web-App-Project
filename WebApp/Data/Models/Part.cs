using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static WebApp.Data.DataConstants.Part;

namespace WebApp.Data.Models
{
    public class Part
    { 
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Column(TypeName = PriceFormat)]
        public decimal Price { get; set; }

                                        //?
        public string ProviderId { get; init; }

        public Provider Provider { get; init; }

        [Required]                                      //?
        public string RepairId { get; init; }

        public Repair Repair { get; init; }
    }
}
