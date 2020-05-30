using BLL.Models;
using System;

namespace UI.ViewModels
{
    public class StatementViewModel
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public int SemesterNumber { get; set; }

        public int SemesterYear { get; set; }

        public string SubjectName { get; set; }

        public int Mark { get; set; }

        public TypeOfSertification TypeOfSertification { get; set; }
    }
}
