using DAL.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFramework
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<GroupDTO> Groups { get; set; }

        public DbSet<SemesterDTO> Semesters { get; set; }

        public DbSet<SpecialtyDTO> Specialties { get; set; }

        public DbSet<StatementDTO> Statements { get; set; }

        public DbSet<StudentDTO> Students { get; set; }

        public DbSet<SubjectDTO> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupDTO>()
                .HasOne<SpecialtyDTO>()
                .WithMany()
                .HasForeignKey(group => group.SpecialtyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StatementDTO>()
                .HasOne<StudentDTO>()
                .WithMany()
                .HasForeignKey(statement => statement.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StatementDTO>()
                .HasOne<SemesterDTO>()
                .WithMany()
                .HasForeignKey(statement => statement.SemesterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StatementDTO>()
                .HasOne<SubjectDTO>()
                .WithMany()
                .HasForeignKey(statement => statement.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentDTO>()
                .HasOne<GroupDTO>()
                .WithMany()
                .HasForeignKey(student => student.GroupId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
