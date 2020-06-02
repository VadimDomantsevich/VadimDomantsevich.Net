using BLL.Models;
using BLL.Services;
using DAL.DTO;
using System;
using System.Threading.Tasks;
using UI.Helpers;
using UI.Interfaces;

namespace UI.Services.CrudConsoleService
{
    public class StatementsCrudConsoleService : ICrudConsoleService<Statement>
    {
        private readonly StatementService _statementService;
        private readonly StudentService _studentService;
        private readonly SubjectService _subjectService;
        private readonly SemesterService _semesterService;

        public StatementsCrudConsoleService(
            StatementService statementService,
            StudentService studentService,
            SubjectService subjectService,
            SemesterService semesterService)
        {
            _statementService = statementService;
            _studentService = studentService;
            _subjectService = subjectService;
            _semesterService = semesterService;
        }

        public async Task Create()
        {
            var statement = await CreateModel();

            await _statementService.Create(statement);
        }

        public async Task<Statement> CreateModel()
        {
            return await Task.Run( async () => 
            {
                (await _studentService.GetAll()).WriteCollectionAsTable();
                Console.WriteLine("Enter Id of student:");
                var studentId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                (await _subjectService.GetAll()).WriteCollectionAsTable();
                Console.WriteLine("Enter Id of subject:");
                var subjectId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                (await _semesterService.GetAll()).WriteCollectionAsTable();
                Console.WriteLine("Enter Id of semester:");
                var semesterId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                Console.WriteLine("Choose type of sertification:");
                Console.WriteLine("1. Exam");
                Console.WriteLine("2. Colloqeum");
                Console.WriteLine("3. Test");
                var typeOfSertification = (TypeOfSertification)(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()) - 1);

                Console.WriteLine("Enter mark:");
                var mark = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                var statement = new Statement
                {
                    StudentId = studentId,
                    SubjectId = subjectId,
                    SemesterId = semesterId,
                    TypeOfSertification = typeOfSertification,
                    Mark = mark
                };

                return statement;
            });
        }

        public async Task Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            await _statementService.Delete(id);
        }

        public async Task Update()
        {
            Console.WriteLine("Enter Id of statement to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var statement = await CreateModel();
            statement.Id = id;

            await _statementService.Update(statement);
        }
    }
}
