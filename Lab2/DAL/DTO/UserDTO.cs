using Microsoft.AspNetCore.Identity;

namespace DAL.DTO
{
    public class UserDTO : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
