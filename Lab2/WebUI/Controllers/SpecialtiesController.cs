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
    public class SpecialtiesController : Controller
    {
        private readonly ISpecialtiesApi _specialtiesApi;
        private readonly ILogger<SpecialtiesController> _logger;

        public SpecialtiesController(ISpecialtiesApi specialtiesApi, ILogger<SpecialtiesController> logger)
        {
            _specialtiesApi = specialtiesApi;
            _logger = logger;
        }

        // GET: Specialties
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var specialtyViewModels = (await _specialtiesApi.GetAll()).Select(CreateSpecialtyViewModel);
            return View(specialtyViewModels);
        }

        // GET: Specialties/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var specialty = await _specialtiesApi.GetById(id);

            var specialtyViewModel = CreateSpecialtyViewModel(specialty);

            return View(specialtyViewModel);
        }

        // GET: Specialties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialtyViewModel specialtyViewModel)
        {
            try
            {
                await _specialtiesApi.Add(new Specialty
                {
                    Id = specialtyViewModel.Id,
                    Name = specialtyViewModel.Name
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating specialty. Exception: {exception.Message}");
                return View(specialtyViewModel);
            }
        }

        // GET: Specialties/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var specialty = await _specialtiesApi.GetById(id);

            var specialtyViewModel = CreateSpecialtyViewModel(specialty);

            return View(specialtyViewModel);
        }

        // POST: Specialties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialtyViewModel categoryViewModel)
        {
            try
            {
                await _specialtiesApi.Update(new Specialty
                {
                    Id = categoryViewModel.Id,
                    Name = categoryViewModel.Name
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating specialty. Exception: {exception.Message}");
                return View(categoryViewModel);
            }
        }

        // GET: Specialties/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var specialty = await _specialtiesApi.GetById(id);

            var specialtyViewModel = CreateSpecialtyViewModel(specialty);

            return View(specialtyViewModel);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SpecialtyViewModel specialtyViewModel)
        {
            try
            {
                await _specialtiesApi.Delete(specialtyViewModel.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting specialty. Exception: {exception.Message}");
                return View(specialtyViewModel);
            }
        }

        private SpecialtyViewModel CreateSpecialtyViewModel(Specialty specialty)
        {
            return new SpecialtyViewModel
            {
                Id = specialty.Id,
                Name = specialty.Name
            };
        }
    }
}