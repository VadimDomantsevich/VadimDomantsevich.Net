using BLL.Models;
using BLL.Services;
using System;
using UI.Interfaces;

namespace UI.View.CrudConsoleService
{
    public class SemesterCrudConsoleService : ICrudConsoleService<Semester>
    {
        private readonly SemesterServices _semesterService;

        public SemesterCrudConsoleService(SemesterServices semesterService)
        {
            _semesterService = semesterService;
        }

        public void Create()
        {
            var semester = CreateModel();

            _semesterService.Create(semester);
        }

        public Semester CreateModel()
        {
            Console.WriteLine("Enter semester number:");
            var semesterNumber = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            Console.WriteLine("Enter semester year:");
            var year = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var semester = new Semester
            {
                Number = semesterNumber,
                Year = year
            };

            return semester;
        }

        public void Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            _semesterService.Delete(id);
        }

        public void Update()
        {
            Console.WriteLine("Enter Id of semester to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var semester = CreateModel();
            semester.Id = id;

            _semesterService.Update(semester);
        }
    }
}
