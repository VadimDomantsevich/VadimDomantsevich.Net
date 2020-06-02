using BLL.Services;
using UI.Interfaces;

namespace UI.View.PrintConsoleService
{
    public class SubjectPrintConsoleService : IPrintConsoleService
    {
        private readonly SubjectServices _subjectService;

        public SubjectPrintConsoleService(SubjectServices subjectService)
        {
            _subjectService = subjectService;
        }

        public void PrintAll()
        {
            var items = _subjectService.GetAll();

            items.WriteCollectionAsTable();
        }
    }
}
