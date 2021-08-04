using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static WebApp.Data.DataConstants.RepairType;

namespace WebApp.Data.Models
{
    public class RepairType
    {  
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Repair> Repairs { get; set; } = new List<Repair>();
    }
}
