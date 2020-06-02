using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class StatementViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public string StudentFullName { get; set; }

        [Required]
        public int SemesterId { get; set; }

        [Required]
        public int SemesterNumber { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public string SubjectName { get; set; }

        [Required]
        [Range(1, 10)]
        public int Mark { get; set; }

        [Required]
        public string TypeOfSertification { get; set; }

        public SelectList Students { get; set; }

        public SelectList Semesters { get; set; }

        public SelectList Subjects { get; set; }
    }
}
