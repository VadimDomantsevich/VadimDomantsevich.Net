using BLL.Services;
using System.Linq;
using UI.Interfaces;
using UI.ViewModels;

namespace UI.View.PrintConsoleService
{
    public class GroupPrintConsoleService : IPrintConsoleService
    {
        private readonly GroupServices _groupService;
        private readonly SpecialtyServices _specialtyService;

        public GroupPrintConsoleService(GroupServices groupService, SpecialtyServices specialtyService)
        {
            _groupService = groupService;
            _specialtyService = specialtyService;
        }

        public void PrintAll()
        {
            var specialities = _specialtyService.GetAll();

            var items = (_groupService.GetAll())
                .Select(item => new GroupViewModel
                {
                    Name = item.Name,
                    SpecialtyName = specialities.First(specialty => specialty.Id == item.SpecialtyId).Name,
                });

            items.WriteCollectionAsTable();
        }
    }
}
