using BLL.Models;
using BLL.Services;
using System;
using UI.Interfaces;

namespace UI.View.CrudConsoleService
{
    public class StatementCrudConsoleService : ICrudConsoleService<Statement>
    {
        private readonly StatementServices _statementService;
        private readonly StudentServices _studentService;
        private readonly SubjectServices _subjectService;
        private readonly SemesterServices _semesterService;

        public StatementCrudConsoleService(
            StatementServices statementService,
            StudentServices studentService,
            SubjectServices subjectService,
            SemesterServices semesterService)
        {
            _statementService = statementService;
            _studentService = studentService;
            _subjectService = subjectService;
            _semesterService = semesterService;
        }

        public void Create()
        {
            var statement = CreateModel();

            _statementService.Create(statement);
        }

        public Statement CreateModel()
        {
            _studentService.GetAll().WriteCollectionAsTable();
            Console.WriteLine("Enter Id of student:");
            var studentId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _subjectService.GetAll().WriteCollectionAsTable();
            Console.WriteLine("Enter Id of subject:");
            var subjectId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _semesterService.GetAll().WriteCollectionAsTable();
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
        }

        public void Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            _statementService.Delete(id);
        }

        public void Update()
        {
            Console.WriteLine("Enter Id of statement to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var statement = CreateModel();
            statement.Id = id;

            _statementService.Update(statement);
        }
    }
}
