using BLL.Models;
using BLL.Services;
using System;
using UI.Interfaces;

namespace UI.View.CrudConsoleService
{
    public class StudentCrudConsoleService : ICrudConsoleService<Student>
    {
        private readonly StudentServices _studentService;
        private readonly GroupServices _groupService;

        public StudentCrudConsoleService(StudentServices studentService, GroupServices groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        public void Create()
        {
            var student = CreateModel();

            _studentService.Create(student);
        }

        public Student CreateModel()
        {
            Console.WriteLine("Enter student full name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter student record number:");
            var recordNumber = Console.ReadLine();
            Console.WriteLine("Enter student phone number:");
            var phoneNumber = Console.ReadLine();
            _groupService.GetAll().WriteCollectionAsTable();
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
        }

        public void Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            _studentService.Delete(id);
        }

        public void Update()
        {
            Console.WriteLine("Enter Id of student to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var student = CreateModel();
            student.Id = id;

            _studentService.Update(student);
        }
    }
}
