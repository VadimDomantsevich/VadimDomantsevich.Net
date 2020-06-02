using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly SubjectService _subjectService;
        private readonly ILogger<SubjectsController> _logger;

        public SubjectsController(SubjectService subjectService, ILogger<SubjectsController> logger)
        {
            _subjectService = subjectService;
            _logger = logger;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var subjectViewModels = (await _subjectService.GetAll()).Select(CreateSubjectViewModel);
            return View(subjectViewModels);
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var subject = await _subjectService.GetById(id);

            var subjectViewModel = CreateSubjectViewModel(subject);

            return View(subjectViewModel);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectViewModel subjectViewModel)
        {
            try
            {
                await _subjectService.Create(new Subject
                {
                    Id = subjectViewModel.Id,
                    Name = subjectViewModel.Name
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating subject. Exception: {exception.Message}");
                return View(subjectViewModel);
            }
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var subject = await _subjectService.GetById(id);

            var subjectViewModel = CreateSubjectViewModel(subject);

            return View(subjectViewModel);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubjectViewModel subjectViewModel)
        {
            try
            {
                await _subjectService.Update(new Subject
                {
                    Id = subjectViewModel.Id,
                    Name = subjectViewModel.Name
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating subject. Exception: {exception.Message}");
                return View(subjectViewModel);
            }
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _subjectService.GetById(id);

            var subjectViewModel = CreateSubjectViewModel(subject);

            return View(subjectViewModel);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SubjectViewModel subjectViewModel)
        {
            try
            {
                await _subjectService.Delete(subjectViewModel.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting subject. Exception: {exception.Message}");
                return View(subjectViewModel);
            }
        }

        private SubjectViewModel CreateSubjectViewModel(Subject subject)
        {
            return new SubjectViewModel
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }
    }
}
