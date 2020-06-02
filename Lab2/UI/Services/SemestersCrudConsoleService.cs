using BLL.Models;
using BLL.Services;
using System;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
{
    public class SemestersCrudConsoleService : ICrudConsoleService<Semester>
    {
        private readonly SemesterService _semesterService;

        public SemestersCrudConsoleService(SemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        public async Task Create()
        {
            var semester = await CreateModel();

            await _semesterService.Create(semester);
        }

        public async Task<Semester> CreateModel()
        {
            return await Task.Run(() => 
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
            });
        }

        public async Task Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            await _semesterService.Delete(id);
        }

        public async Task Update()
        {
            Console.WriteLine("Enter Id of semester to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var semester = await CreateModel();
            semester.Id = id;

            await _semesterService.Update(semester);
        }
    }
}
