using BLL.Services;
using System.Linq;
using System.Threading.Tasks;
using UI.Helpers;
using UI.Interfaces;
using UI.ViewModels;

namespace UI.Services.PrintConsoleService
{
    public class GroupsPrintConsoleService : IPrintConsoleService
    {
        private readonly GroupService _groupService;
        private readonly SpecialtyService _specialtyService;

        public GroupsPrintConsoleService(GroupService groupService, SpecialtyService specialtyService)
        {
            _groupService = groupService;
            _specialtyService = specialtyService;
        }

        public async Task PrintAll()
        {
            var specialities = await _specialtyService.GetAll();

            var items = (await _groupService.GetAll())
                .Select(item => new GroupViewModel
                {
                    Name = item.Name,
                    SpecialtyName = specialities.First(specialty => specialty.Id == item.SpecialtyId).Name,
                });

            items.WriteCollectionAsTable();
        }
    }
}
