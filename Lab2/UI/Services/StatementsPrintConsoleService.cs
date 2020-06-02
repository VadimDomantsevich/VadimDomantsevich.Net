using BLL.Services;
using System.Linq;
using System.Threading.Tasks;
using UI.Helpers;
using UI.Interfaces;
using UI.ViewModels;

namespace UI.Services.PrintConsoleService
{
    public class StatementsPrintConsoleService : IPrintConsoleService
    {
        private readonly StatementService _statementService;
        private readonly StudentService _studentService;
        private readonly SemesterService _semesterService;
        private readonly SubjectService _subjectService;

        public StatementsPrintConsoleService(
            StatementService statementService,
            StudentService studentService,
            SemesterService semesterService,
            SubjectService subjectService)
        {
            _statementService = statementService;
            _studentService = studentService;
            _semesterService = semesterService;
            _subjectService = subjectService;
        }

        public async Task PrintAll()
        {
            var students = await _studentService.GetAll();
            var semesters = await _semesterService.GetAll();
            var subjects = await _subjectService.GetAll();

            var items = (await _statementService.GetAll())
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
