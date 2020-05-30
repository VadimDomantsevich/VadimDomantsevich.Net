using BLL.Infrastructure;
using BLL.Models;
using System;
using UI.Interfaces;
using UI.View.PrintConsoleService;

namespace UI.View.MainConsoleService
{
    public class SpecialtyBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Specialty> _crudSpecialtyService;
        private readonly IPrintConsoleService _printSpecialtyService;

        public SpecialtyBaseConsoleService(ICrudConsoleService<Specialty> crudSpecialtyService, SpecialtyPrintConsoleService printSpecialtyService)
        {
            _crudSpecialtyService = crudSpecialtyService;
            _printSpecialtyService = printSpecialtyService;
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Add specialty");
            Console.WriteLine("2. Delete specialty");
            Console.WriteLine("3. Update specialty");
            Console.WriteLine("4. Back");
        }

        public void StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    _printSpecialtyService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            _crudSpecialtyService.Create();
                            break;
                        case 2:
                            _crudSpecialtyService.Delete();
                            break;
                        case 3:
                            _crudSpecialtyService.Update();
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
