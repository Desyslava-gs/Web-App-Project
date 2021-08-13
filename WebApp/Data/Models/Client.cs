using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static WebApp.Data.DataConstants.Client;

namespace WebApp.Data.Models
{
    public class Client
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

    //    public User User { get; set; }

        public IEnumerable<Car> Cars { get; init; } = new List<Car>();
    }
}
