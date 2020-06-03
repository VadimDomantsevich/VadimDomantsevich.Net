using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Identity;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize(Roles = Roles.Manager)]
    public class StatementsController : Controller
    {
        private readonly StatementService _statementService;
        private readonly StudentService _studentService;
        private readonly ILogger<StatementsController> _logger;
        private readonly SemesterService _semesterService;
        private readonly SubjectService _subjectService;

        public StatementsController(
            StatementService statementService,
            StudentService studentService,
            ILogger<StatementsController> logger,
            SemesterService semesterService,
            SubjectService subjectService)
        {
            _statementService = statementService;
            _studentService = studentService;
            _logger = logger;
            _semesterService = semesterService;
            _subjectService = subjectService;
        }


        // GET: Statements
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var students = await _studentService.GetAll();
            var subjects = await _subjectService.GetAll();
            var semesters = await _semesterService.GetAll();
            var statementViewModels = (await _statementService.GetAll())
                                    .Select(statement => CreateStatementViewModel(statement, students, subjects, semesters))
                                    .OrderBy(statement => statement.Mark);

            return View(statementViewModels);
        }

        // GET: Statements/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var students = await _studentService.GetAll();
            var subjects = await _subjectService.GetAll();
            var semesters = await _semesterService.GetAll();
            var statement = await _statementService.GetById(id);

            var statementViewModel = CreateStatementViewModel(statement, students, subjects, semesters);

            return View(statementViewModel);
        }

        // GET: Statements/Create
        public async Task<ActionResult> Create()
        {
            var statementViewModel = new StatementViewModel
            {
                Students = new SelectList(await _studentService.GetAll(), "Id", "FullName"),
                Semesters = new SelectList(await _semesterService.GetAll(), "Id", "Number"),
                Subjects = new SelectList(await _subjectService.GetAll(), "Id", "Name")
            };

            return View(statementViewModel);
        }

        // POST: Statements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StatementViewModel statementViewModel)
        {
            try
            {
                await _statementService.Create(new Statement
                {
                    TypeOfSertification = statementViewModel.TypeOfSertification,
                    Mark = statementViewModel.Mark,
                    SemesterId = statementViewModel.SemesterId,
                    StudentId = statementViewModel.StudentId,
                    SubjectId = statementViewModel.SubjectId
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating statement. Exception: {exception.Message}");
                statementViewModel.Subjects = new SelectList(await _subjectService.GetAll(), "Id", "Name");
                statementViewModel.Students = new SelectList(await _studentService.GetAll(), "Id", "FullName");
                statementViewModel.Semesters = new SelectList(await _semesterService.GetAll(), "Id", "Number");
                return View(statementViewModel);
            }
        }

        // GET: Statements/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var students = await _studentService.GetAll();
            var subjects = await _subjectService.GetAll();
            var semesters = await _semesterService.GetAll();
            var statement = await _statementService.GetById(id);

            var statementViewModel = CreateStatementViewModel(statement, students, subjects, semesters);

            statementViewModel.Students = new SelectList(await _studentService.GetAll(), "Id", "FullName");
            statementViewModel.Semesters = new SelectList(await _semesterService.GetAll(), "Id", "Number");
            statementViewModel.Subjects = new SelectList(await _subjectService.GetAll(), "Id", "Name");

            return View(statementViewModel);
        }

        // POST: Statements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StatementViewModel statementViewModel)
        {
            try
            {
                await _statementService.Update(new Statement
                {
                    Id = statementViewModel.Id,
                    TypeOfSertification = statementViewModel.TypeOfSertification,
                    Mark = statementViewModel.Mark,
                    SubjectId = statementViewModel.SubjectId,
                    StudentId = statementViewModel.StudentId,
                    SemesterId = statementViewModel.SemesterId
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing statement. Exception: {exception.Message}");
                statementViewModel.Students = new SelectList(await _studentService.GetAll(), "Id", "FullName");
                statementViewModel.Subjects = new SelectList(await _subjectService.GetAll(), "Id", "Name");
                statementViewModel.Semesters = new SelectList(await _semesterService.GetAll(), "Id", "Number");
                return View(statementViewModel);
            }
        }

        // GET: Statements/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var students = await _studentService.GetAll();
            var subjects = await _subjectService.GetAll();
            var semesters = await _semesterService.GetAll();
            var statement = await _statementService.GetById(id);
            var statementViewModel = CreateStatementViewModel(statement, students, subjects, semesters);
            return View(statementViewModel);
        }

        // POST: Statements/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(StatementViewModel statement)
        {
            try
            {
                await _statementService.Delete(statement.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting statement. Exception: {exception.Message}");
                return View(statement);
            }
        }

        private StatementViewModel CreateStatementViewModel(
            Statement statement,
            IEnumerable<Student> students,
            IEnumerable<Subject> subjects,
            IEnumerable<Semester> semesters)
        {
            return new StatementViewModel
            {
                Id = statement.Id,
                TypeOfSertification = statement.TypeOfSertification,
                StudentId = statement.StudentId,
                SubjectId = statement.SubjectId,
                SemesterId = statement.SemesterId,
                Mark = statement.Mark,
                SemesterNumber = semesters.First(semester => semester.Id == statement.SemesterId).Number,
                SubjectName = subjects.First(subject => subject.Id == statement.SubjectId).Name,
                StudentFullName = students.First(students => students.Id == statement.StudentId).FullName
            };
        }
    }
}
