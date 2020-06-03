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
using WebUI.Api;
using WebUI.Identity;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize(Roles = Roles.Manager)]
    public class StudentsController : Controller
    {
        private readonly IStudentsApi _studentsApi;
        private readonly IGroupsApi _groupsApi;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentsApi studentsApi, IGroupsApi groupsApi, ILogger<StudentsController> logger)
        {
            _studentsApi = studentsApi;
            _groupsApi = groupsApi;
            _logger = logger;
        }

        // GET: Student
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var groups = await _groupsApi.GetAll();
            var students = (await _studentsApi.GetAll())
                                  .Select(student => CreateStudentViewModel(student, groups))
                                  .OrderBy(student => student.GroupName);

            return View(students);
        }

        // GET: Student/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var groups = await _groupsApi.GetAll();
            var student = CreateStudentViewModel(await _studentsApi.GetById(id), groups);

            return View(student);
        }

        // GET: Student/Create
        public async Task<ActionResult> Create()
        {
            var studentViewModel = new StudentViewModel
            {
                Groups = new SelectList(await _groupsApi.GetAll(), "Id", "Name")
            };

            return View(studentViewModel);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentViewModel studentViewModel)
        {
            try
            {
                await _studentsApi.Add(new Student
                {
                    FullName = studentViewModel.FullName,
                    PhoneNumber = studentViewModel.PhoneNumber,
                    RecordNumber = studentViewModel.RecordNumber,
                    GroupId = studentViewModel.GroupId
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating student. Exception: {exception.Message}");
                studentViewModel.Groups = new SelectList(await _groupsApi.GetAll(), "Id", "Name");
                return View(studentViewModel);
            }
        }

        // GET: Student/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var groups = await _groupsApi.GetAll();
            var studentViewModel = CreateStudentViewModel(await _studentsApi.GetById(id), groups);
            studentViewModel.Groups = new SelectList(await _groupsApi.GetAll(), "Id", "Name");
            return View(studentViewModel);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentViewModel studentViewModel)
        {
            try
            {
                await _studentsApi.Update(new Student
                {
                    Id = studentViewModel.Id,
                    FullName = studentViewModel.FullName,
                    PhoneNumber = studentViewModel.PhoneNumber,
                    RecordNumber = studentViewModel.RecordNumber,
                    GroupId = studentViewModel.GroupId
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing student. Exception: {exception.Message}");
                studentViewModel.Groups = new SelectList(await _groupsApi.GetAll(), "Id", "Name");
                return View(studentViewModel);
            }
        }

        // GET: Student/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var groups = await _groupsApi.GetAll();
            var studentViewModel = CreateStudentViewModel(await _studentsApi.GetById(id), groups);
            return View(studentViewModel);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(StudentViewModel studentViewModel)
        {
            try
            {
                await _studentsApi.Delete(studentViewModel.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting student. Exception: {exception.Message}");
                return View(studentViewModel);
            }
        }

        private StudentViewModel CreateStudentViewModel(Student student, IEnumerable<Group> groups)
        {
            return new StudentViewModel
            {
                Id = student.Id,
                FullName = student.FullName,
                PhoneNumber = student.PhoneNumber,
                RecordNumber = student.RecordNumber,
                GroupId = student.GroupId,
                GroupName = groups.First(group => group.Id == student.GroupId).Name
            };
        }
    }
}
