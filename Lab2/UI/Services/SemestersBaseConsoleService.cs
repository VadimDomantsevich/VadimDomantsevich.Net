using BLL.Infrastructure;
using BLL.Models;
using System;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
{
    public class SemestersBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Semester> _crudSemesterService;
        private readonly IPrintConsoleService _printSemesterService;

        public SemestersBaseConsoleService(ICrudConsoleService<Semester> crudSemesterService, SemestersPrintConsoleService printSemesterService)
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

        public async Task StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printSemesterService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            await _crudSemesterService.Create();
                            break;
                        case 2:
                            await _crudSemesterService.Delete();
                            break;
                        case 3:
                            await _crudSemesterService.Update();
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
