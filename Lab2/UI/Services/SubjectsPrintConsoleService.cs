using BLL.Services;
using System.Threading.Tasks;
using UI.Helpers;
using UI.Interfaces;

namespace UI.Services
{
    public class SubjectsPrintConsoleService : IPrintConsoleService
    {
        private readonly SubjectService _subjectService;

        public SubjectsPrintConsoleService(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public async Task PrintAll()
        {
            var items = await _subjectService.GetAll();

            items.WriteCollectionAsTable();
        }
    }
}
