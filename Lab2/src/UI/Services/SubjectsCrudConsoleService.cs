using BLL.Models;
using BLL.Services;
using System;
using UI.Interfaces;

namespace UI.View.CrudConsoleService
{
    public class SubjectCrudConsoleService : ICrudConsoleService<Subject>
    {
        private readonly SubjectServices _subjectService;

        public SubjectCrudConsoleService(SubjectServices subjectService)
        {
            _subjectService = subjectService;
        }

        public void Create()
        {
            var subject = CreateModel();

            _subjectService.Create(subject);
        }

        public Subject CreateModel()
        {
            Console.WriteLine("Enter subject name:");
            var name = Console.ReadLine();

            var subject = new Subject
            {
                Name = name
            };

            return subject;
        }

        public void Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            _subjectService.Delete(id);
        }

        public void Update()
        {
            Console.WriteLine("Enter Id of subject to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var subject = CreateModel();
            subject.Id = id;

            _subjectService.Update(subject);
        }
    }
}
