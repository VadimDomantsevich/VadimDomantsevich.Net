using BLL.Models;
using BLL.Services;
using System;
using UI.Interfaces;

namespace UI.View.CrudConsoleService
{
    public class GroupCrudConsoleService : ICrudConsoleService<Group>
    {
        private readonly GroupServices _groupService;
        private readonly SpecialtyServices _specialtyService;

        public GroupCrudConsoleService(GroupServices groupService, SpecialtyServices specialtyService)
        {
            _groupService = groupService;
            _specialtyService = specialtyService;
        }

        public void Create()
        {
            var group = CreateModel();

            _groupService.Create(group);
        }

        public Group CreateModel()
        {
            Console.WriteLine("Enter group name:");
            var name = Console.ReadLine();
            _specialtyService.GetAll().WriteCollectionAsTable();
            Console.WriteLine("Enter Id of specialty:");
            var specialtyId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var group = new Group
            {
                Name = name,
                SpecialtyId = specialtyId
            };

            return group;
        }

        public void Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            _groupService.Delete(id);
        }

        public void Update()
        {
            Console.WriteLine("Enter Id of group to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var group = CreateModel();
            group.Id = id;

            _groupService.Update(group);
        }
    }
}
