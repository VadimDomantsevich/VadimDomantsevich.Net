using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class GroupViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        [Required]
        public string SpecialtyName { get; set; }

        public SelectList SpecialtiesSelectList { get; set; }
    }
}
