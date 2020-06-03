using BLL.Services;
using System.Threading.Tasks;
using UI.Helpers;
using UI.Interfaces;

namespace UI.Services
{
    public class SemestersPrintConsoleService : IPrintConsoleService
    {
        private readonly SemesterService _semesterService;

        public SemestersPrintConsoleService(SemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        public async Task PrintAll()
        {
            var items = await _semesterService.GetAll();

            items.WriteCollectionAsTable();
        }
    }
}
