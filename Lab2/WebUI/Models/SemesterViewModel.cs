using System;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class SemesterViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(1, 2)]
        public int Number { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Year { get; set; }
    }
}
