using BLL.Infrastructure;
using BLL.Models;
using System;
using UI.Interfaces;
using UI.View.PrintConsoleService;

namespace UI.View.MainConsoleService
{
    public class StudentBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Student> _crudStudentService;
        private readonly IPrintConsoleService _printStudentService;

        public StudentBaseConsoleService(ICrudConsoleService<Student> crudStudentService, StudentPrintConsoleService printStudentService)
        {
            _crudStudentService = crudStudentService;
            _printStudentService = printStudentService;
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Add student");
            Console.WriteLine("2. Delete student");
            Console.WriteLine("3. Update student");
            Console.WriteLine("4. Back");
        }

        public void StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    _printStudentService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            _crudStudentService.Create();
                            break;
                        case 2:
                            _crudStudentService.Delete();
                            break;
                        case 3:
                            _crudStudentService.Update();
                            break;
                        case 4:
                            return;
                    }
                }
                catch (ValidationException e)
                {
                    Console.WriteLine($"Validation error. Message: {e.Message}");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }
    }
}
