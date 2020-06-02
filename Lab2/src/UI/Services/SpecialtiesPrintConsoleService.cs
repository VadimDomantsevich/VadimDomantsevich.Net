using BLL.Services;
using UI.Interfaces;

namespace UI.View.PrintConsoleService
{
    public class SpecialtyPrintConsoleService : IPrintConsoleService
    {
        private readonly SpecialtyServices _specialtyService;

        public SpecialtyPrintConsoleService(SpecialtyServices specialtyService)
        {
            _specialtyService = specialtyService;
        }

        public void PrintAll()
        {
            var items = _specialtyService.GetAll();

            items.WriteCollectionAsTable();
        }
    }
}
