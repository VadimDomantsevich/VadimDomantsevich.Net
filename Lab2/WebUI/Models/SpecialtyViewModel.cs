using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class SpecialtyViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
