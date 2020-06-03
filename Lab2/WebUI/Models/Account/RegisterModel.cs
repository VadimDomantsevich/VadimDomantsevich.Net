using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Account
{
    public class RegisterModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
