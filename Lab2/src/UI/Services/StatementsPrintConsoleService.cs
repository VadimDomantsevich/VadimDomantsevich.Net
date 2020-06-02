using BLL.Services;
using System.Linq;
using UI.Interfaces;
using UI.ViewModels;

namespace UI.View.PrintConsoleService
{
    public class StatementPrintConsoleService : IPrintConsoleService
    {
        private readonly StatementServices _statementService;
        private readonly StudentServices _studentService;
        private readonly SemesterServices _semesterService;
        private readonly SubjectServices _subjectService;

        public StatementPrintConsoleService(
            StatementServices statementService,
            StudentServices studentService,
            SemesterServices semesterService,
            SubjectServices subjectService)
        {
            _statementService = statementService;
            _studentService = studentService;
            _semesterService = semesterService;
            _subjectService = subjectService;
        }

        public void PrintAll()
        {
            var students = _studentService.GetAll();
            var semesters = _semesterService.GetAll();
            var subjects = _subjectService.GetAll();

            var items = (_statementService.GetAll())
                .Select(item => new StatementViewModel
                {
                    StudentName = students.First(student => student.Id == item.StudentId).FullName,
                    SemesterNumber = semesters.First(semester => semester.Id == item.SemesterId).Number,
                    SemesterYear = semesters.First(semester => semester.Id == item.SemesterId).Year,
                    SubjectName = subjects.First(subject => subject.Id == item.SubjectId).Name,
                    Mark = item.Mark,
                    TypeOfSertification = item.TypeOfSertification
                });

            items.WriteCollectionAsTable();
        }
    }
}
