using BLL.Infrastructure;
using BLL.Models;
using System;
using UI.Interfaces;
using UI.View.PrintConsoleService;

namespace UI.View.MainConsoleService
{
    public class SemesterBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Semester> _crudSemesterService;
        private readonly IPrintConsoleService _printSemesterService;

        public SemesterBaseConsoleService(ICrudConsoleService<Semester> crudSemesterService, SemesterPrintConsoleService printSemesterService)
        {
            _crudSemesterService = crudSemesterService;
            _printSemesterService = printSemesterService;
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Add semester");
            Console.WriteLine("2. Delete semester");
            Console.WriteLine("3. Update semester");
            Console.WriteLine("4. Back");
        }

        public void StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    _printSemesterService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            _crudSemesterService.Create();
                            break;
                        case 2:
                            _crudSemesterService.Delete();
                            break;
                        case 3:
                            _crudSemesterService.Update();
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
