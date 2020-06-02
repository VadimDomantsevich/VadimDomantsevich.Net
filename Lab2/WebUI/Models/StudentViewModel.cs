using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class StudentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("^\\d{6}$")]
        public string RecordNumber { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Required]
        [RegularExpression("^(\\+375|80)(29|25|44|33)(\\d{3})(\\d{2})(\\d{2})$")]
        public string PhoneNumber { get; set; }

        public SelectList Groups { get; set; }
    }
}
