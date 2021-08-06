using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Data.Models
{
    using static DataConstants.Client;

    public class Client
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Car> Cars { get; init; } = new List<Car>();
    }

}
