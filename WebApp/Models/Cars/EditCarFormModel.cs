using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static WebApp.Data.DataConstants.Car;

namespace WebApp.Models.Cars
{
    public class EditCarFormModel
    { 
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(MakeMaxLength, MinimumLength = MakeMinLength)]
        public string Make { get; set; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; set; }
        
        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; set; }

        [Required]
        [StringLength(ColorMaxLength, MinimumLength = ColorMinLength)]
        public string Color { get; set; }

        [Required]
        [MaxLength(PlateNumberMaxLength)]
        [RegularExpression(RegexPlateNumber)]
        public string PlateNumber { get; set; }

        [Required]
        [StringLength(VinNumberMaxLength, MinimumLength = VinNumberMinLength)]
        public string VinNumber { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        [Required]
        [StringLength(
            int.MaxValue, 
            MinimumLength = DescriptionMinLength, 
            ErrorMessage = "The field Description must be minimum length of {2}.")]
        public string Description { get; set; }
        
        public string FuelTypeId { get; set; }

        public IEnumerable<FuelTypeViewModel> FuelTypes { get; set; }
    }
}
