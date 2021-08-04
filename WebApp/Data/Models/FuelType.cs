using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static WebApp.Data.DataConstants.FuelType;

namespace WebApp.Data.Models
{
    public class FuelType
    {
        
            [Key]
            [Required] public string Id { get; init; } = Guid.NewGuid().ToString();
            public string Name { get; set; }

            public IEnumerable<Car> Cars { get; set; } = new List<Car>();
        
    }
}
