using BLL.Models;
using BLL.Services;
using System;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services.CrudConsoleService
{
    public class SubjectsCrudConsoleService : ICrudConsoleService<Subject>
    {
        private readonly SubjectService _subjectService;

        public SubjectsCrudConsoleService(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public async Task Create()
        {
            var subject = await CreateModel();

            await _subjectService.Create(subject);
        }

        public async Task<Subject> CreateModel()
        {
            return await Task.Run(() => 
            {
                Console.WriteLine("Enter subject name:");
                var name = Console.ReadLine();

                var subject = new Subject
                {
                    Name = name
                };

                return subject;
            });
            
        }

        public async Task Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            await _subjectService.Delete(id);
        }

        public async Task Update()
        {
            Console.WriteLine("Enter Id of subject to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var subject = await CreateModel();
            subject.Id = id;

            await _subjectService.Update(subject);
        }
    }
}
