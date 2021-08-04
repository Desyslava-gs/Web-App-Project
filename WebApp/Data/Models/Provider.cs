using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static WebApp.Data.DataConstants.Provider;

namespace WebApp.Data.Models
{
    public class Provider
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } 

        [Required]
        [MaxLength(NameMaxLength)]
        public string Manager { get; set; }

        [Required]
        [MaxLength(CompanyNumberMaxLength)]
        public string CompanyNumber { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

       [Required]
       public string Email { get; set; }


       [Required]
       [MaxLength(PhoneNumberMaxLength)]
       public string PhoneNumber { get; set; }

       [Required]
       [MaxLength(IbanNumberMaxLength)]
       public string Iban { get; set; }

       public IEnumerable<Part> Parts { get; set; } = new List<Part>();

    }
}
