using BLL.Services;
using System.Linq;
using System.Threading.Tasks;
using UI.Helpers;
using UI.Interfaces;
using UI.ViewModels;

namespace UI.Services
{
    public class StudentsPrintConsoleService : IPrintConsoleService
    {
        private readonly StudentService _studentService;
        private readonly GroupService _groupService;

        public StudentsPrintConsoleService(StudentService studentService, GroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        public async Task PrintAll()
        {
            var groups = await _groupService.GetAll();

            var items = (await _studentService.GetAll())
                .Select(item => new StudentViewModel
                {
                    FullName = item.FullName,
                    RecordNumber = item.RecordNumber,
                    PhoneNumber = item.PhoneNumber,
                    GroupName = groups.First(group => group.Id == item.GroupId).Name
                });

            items.WriteCollectionAsTable();
        }
    }
}
