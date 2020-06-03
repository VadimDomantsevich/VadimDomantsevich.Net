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
    public class GroupsController : Controller
    {
        private readonly IGroupsApi _groupsApi;
        private readonly ISpecialtiesApi _specialtiesApi;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(IGroupsApi groupsApi, ISpecialtiesApi specialtiesApi, ILogger<GroupsController> logger)
        {
            _groupsApi = groupsApi;
            _specialtiesApi = specialtiesApi;
            _logger = logger;
        }

        // GET: Groups
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var specialties = await _specialtiesApi.GetAll();
            var groups = (await _groupsApi.GetAll())
                                  .Select(group => CreateGroupViewModel(group, specialties))
                                  .OrderBy(group => group.SpecialtyName);

            return View(groups);
        }

        // GET: Characteristics/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var specialties = await _specialtiesApi.GetAll();
            var group = CreateGroupViewModel(await _groupsApi.GetById(id), specialties);

            return View(group);
        }

        // GET: Characteristics/Create
        public async Task<ActionResult> Create()
        {
            var groupViewModel = new GroupViewModel
            {
                SpecialtiesSelectList = new SelectList(await _specialtiesApi.GetAll(), "Id", "Name")
            };

            return View(groupViewModel);
        }

        // POST: Characteristics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroupViewModel groupViewModel)
        {
            try
            {
                await _groupsApi.Add(new Group
                {
                    SpecialtyId = groupViewModel.SpecialtyId,
                    Name = groupViewModel.Name
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating group. Exception: {exception.Message}");
                groupViewModel.SpecialtiesSelectList = new SelectList(await _specialtiesApi.GetAll(), "Id", "Name");
                return View(groupViewModel);
            }
        }

        // GET: Characteristics/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var specialties = await _specialtiesApi.GetAll();
            var groupViewModel = CreateGroupViewModel(await _groupsApi.GetById(id), specialties);
            groupViewModel.SpecialtiesSelectList = new SelectList(await _specialtiesApi.GetAll(), "Id", "Name");
            return View(groupViewModel);
        }

        // POST: Characteristics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GroupViewModel groupViewModel)
        {
            try
            {
                await _groupsApi.Update(new Group
                {
                    Id = groupViewModel.Id,
                    SpecialtyId = groupViewModel.SpecialtyId,
                    Name = groupViewModel.Name
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing group. Exception: {exception.Message}");
                groupViewModel.SpecialtiesSelectList = new SelectList(await _specialtiesApi.GetAll(), "Id", "Name");
                return View(groupViewModel);
            }
        }

        // GET: Characteristics/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var specialties = await _specialtiesApi.GetAll();

            var groupViewModel = CreateGroupViewModel(await _groupsApi.GetById(id), specialties);

            return View(groupViewModel);
        }

        // POST: Characteristics/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(GroupViewModel groupViewModel)
        {
            try
            {
                await _groupsApi.Delete(groupViewModel.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting group. Exception: {exception.Message}");
                return View(groupViewModel);
            }
        }

        private GroupViewModel CreateGroupViewModel(Group group, IEnumerable<Specialty> specialties)
        {
            return new GroupViewModel
            {
                Id = group.Id,
                Name = group.Name,
                SpecialtyId = group.SpecialtyId,
                SpecialtyName = specialties.First(specialty => specialty.Id == group.SpecialtyId).Name
            };
        }
    }
}