using BLL.Models;
using BLL.Services;
using System;
using System.Threading.Tasks;
using UI.Helpers;
using UI.Interfaces;

namespace UI.Services.CrudConsoleService
{
    public class StudentsCrudConsoleService : ICrudConsoleService<Student>
    {
        private readonly StudentService _studentService;
        private readonly GroupService _groupService;

        public StudentsCrudConsoleService(StudentService studentService, GroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        public async Task Create()
        {
            var student = await CreateModel();

            await _studentService.Create(student);
        }

        public async Task<Student> CreateModel()
        {
            return await Task.Run( async () => 
            {
                Console.WriteLine("Enter student full name:");
                var name = Console.ReadLine();
                Console.WriteLine("Enter student record number:");
                var recordNumber = Console.ReadLine();
                Console.WriteLine("Enter student phone number:");
                var phoneNumber = Console.ReadLine();
                (await _groupService.GetAll()).WriteCollectionAsTable();
                Console.WriteLine("Enter Id of group:");
                var groupId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                var student = new Student
                {
                    FullName = name,
                    RecordNumber = recordNumber,
                    PhoneNumber = phoneNumber,
                    GroupId = groupId
                };

                return student;
            });
        }

        public async Task Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            await _studentService.Delete(id);
        }

        public async Task Update()
        {
            Console.WriteLine("Enter Id of student to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var student = await CreateModel();
            student.Id = id;

            await _studentService.Update(student);
        }
    }
}
