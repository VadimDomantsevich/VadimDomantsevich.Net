using BLL.Services;
using System.Linq;
using UI.Interfaces;
using UI.ViewModels;

namespace UI.View.PrintConsoleService
{
    public class StudentPrintConsoleService : IPrintConsoleService
    {
        private readonly StudentServices _studentService;
        private readonly GroupServices _groupService;

        public StudentPrintConsoleService(StudentServices studentService, GroupServices groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        public void PrintAll()
        {
            var groups = _groupService.GetAll();

            var items = (_studentService.GetAll())
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
