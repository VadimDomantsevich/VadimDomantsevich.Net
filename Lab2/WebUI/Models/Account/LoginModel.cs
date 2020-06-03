using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Account
{
    public class LoginModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
