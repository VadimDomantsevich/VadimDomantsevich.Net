using BLL.Services;
using System.Threading.Tasks;
using UI.Helpers;
using UI.Interfaces;

namespace UI.Services.PrintConsoleService
{
    public class SpecialtiesPrintConsoleService : IPrintConsoleService
    {
        private readonly SpecialtyService _specialtyService;

        public SpecialtiesPrintConsoleService(SpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        public async Task PrintAll()
        {
            var items = await _specialtyService.GetAll();

            items.WriteCollectionAsTable();
        }
    }
}
