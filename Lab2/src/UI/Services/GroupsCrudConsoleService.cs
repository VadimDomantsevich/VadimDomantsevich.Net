using BLL.Models;
using BLL.Services;
using System;
using System.Threading.Tasks;
using UI.Helpers;
using UI.Interfaces;

namespace UI.Services.CrudConsoleService
{
    public class GroupsCrudConsoleService : ICrudConsoleService<Group>
    {
        private readonly GroupService _groupService;
        private readonly SpecialtyService _specialtyService;

        public GroupsCrudConsoleService(GroupService groupService, SpecialtyService specialtyService)
        {
            _groupService = groupService;
            _specialtyService = specialtyService;
        }

        public async Task Create()
        {
            var group = await CreateModel();

            await _groupService.Create(group);
        }

        public async Task<Group> CreateModel()
        {
            return await Task.Run( async () => 
            {
                Console.WriteLine("Enter group name:");
                var name = Console.ReadLine();
                (await _specialtyService.GetAll()).WriteCollectionAsTable();
                Console.WriteLine("Enter Id of specialty:");
                var specialtyId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                var group = new Group
                {
                    Name = name,
                    SpecialtyId = specialtyId
                };

                return group;
            });
        }

        public async Task Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            await _groupService.Delete(id);
        }

        public async Task Update()
        {
            Console.WriteLine("Enter Id of group to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var group = await CreateModel();
            group.Id = id;

            await _groupService.Update(group);
        }
    }
}
