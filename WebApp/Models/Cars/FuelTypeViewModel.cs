using System.ComponentModel.DataAnnotations;
using static WebApp.Data.DataConstants.FuelType;

namespace WebApp.Models.Cars
{
    public class FuelTypeViewModel
    {
        public string Id { get; set; }
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }
    }
}
