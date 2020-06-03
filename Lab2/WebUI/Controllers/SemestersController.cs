using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Api;
using WebUI.Identity;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize(Roles = Roles.Manager)]
    public class SemestersController : Controller
    {
        private readonly ISemestersApi _semestersApi;
        private readonly ILogger<SemestersController> _logger;

        public SemestersController(ISemestersApi semestersApi, ILogger<SemestersController> logger)
        {
            _semestersApi = semestersApi;
            _logger = logger;
        }

        // GET: Semesters
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var semesterViewModels = (await _semestersApi.GetAll()).Select(CreateSemesterViewModel);
            return View(semesterViewModels);
        }

        // GET: Semesters/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var semester = await _semestersApi.GetById(id);

            var semesterViewModel = CreateSemesterViewModel(semester);

            return View(semesterViewModel);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SemesterViewModel semesterViewModel)
        {
            try
            {
                await _semestersApi.Add(new Semester
                {
                    Id = semesterViewModel.Id,
                    Number = semesterViewModel.Number,
                    Year = semesterViewModel.Year
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating semester. Exception: {exception.Message}");
                return View(semesterViewModel);
            }
        }

        // GET: Semesters/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var semester = await _semestersApi.GetById(id);

            var semesterViewModel = CreateSemesterViewModel(semester);

            return View(semesterViewModel);
        }

        // POST: Semesters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SemesterViewModel semesterViewModel)
        {
            try
            {
                await _semestersApi.Update(new Semester
                {
                    Id = semesterViewModel.Id,
                    Number = semesterViewModel.Number,
                    Year = semesterViewModel.Year
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating semester. Exception: {exception.Message}");
                return View(semesterViewModel);
            }
        }

        // GET: Semesters/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var semester = await _semestersApi.GetById(id);

            var semesterViewModel = CreateSemesterViewModel(semester);

            return View(semesterViewModel);
        }

        // POST: Semesters/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SemesterViewModel semesterViewModel)
        {
            try
            {
                await _semestersApi.Delete(semesterViewModel.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting semester. Exception: {exception.Message}");
                return View(semesterViewModel);
            }
        }

        private SemesterViewModel CreateSemesterViewModel(Semester semester)
        {
            return new SemesterViewModel
            {
                Id = semester.Id,
                Number = semester.Number,
                Year = semester.Year
            };
        }
    }
}