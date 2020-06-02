using BLL.Infrastructure;
using BLL.Models;
using System;
using System.Threading.Tasks;
using UI.Interfaces;
using UI.Services.PrintConsoleService;

namespace UI.Services.MainConsoleService
{
    public class StudentsBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Student> _crudStudentService;
        private readonly IPrintConsoleService _printStudentService;

        public StudentsBaseConsoleService(ICrudConsoleService<Student> crudStudentService, StudentsPrintConsoleService printStudentService)
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

        public async Task StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printStudentService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            await _crudStudentService.Create();
                            break;
                        case 2:
                            await _crudStudentService.Delete();
                            break;
                        case 3:
                            await _crudStudentService.Update();
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
