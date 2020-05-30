using BLL.Services;
using UI.Interfaces;

namespace UI.View.PrintConsoleService
{
    public class SemesterPrintConsoleService : IPrintConsoleService
    {
        private readonly SemesterServices _semesterService;

        public SemesterPrintConsoleService(SemesterServices semesterService)
        {
            _semesterService = semesterService;
        }

        public void PrintAll()
        {
            var items = _semesterService.GetAll();

            items.WriteCollectionAsTable();
        }
    }
}
