namespace DAL.DTO
{
    public class StatementDTO
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int SemesterId { get; set; }

        public int SubjectId { get; set; }

        public int Mark { get; set; }

        public TypeOfSertification TypeOfSertification { get; set; }
    }
}
