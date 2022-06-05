using Microsoft.AspNetCore.Identity;

namespace FiorelloTask.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
