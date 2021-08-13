using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static WebApp.Data.DataConstants.User;

namespace WebApp.Data.Models
{
    public class User:IdentityUser
    {
        [MaxLength(UserNameMaxLength)]
        public string NameUser { get; set; }

        public string ClientId { get; set; }
    }
}
