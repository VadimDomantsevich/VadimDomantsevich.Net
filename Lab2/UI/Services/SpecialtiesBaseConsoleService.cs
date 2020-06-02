using BLL.Infrastructure;
using BLL.Models;
using System;
using System.Threading.Tasks;
using UI.Interfaces;
using UI.Services.PrintConsoleService;

namespace UI.Services.MainConsoleService
{
    public class SpecialtiesBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Specialty> _crudSpecialtyService;
        private readonly IPrintConsoleService _printSpecialtyService;

        public SpecialtiesBaseConsoleService(ICrudConsoleService<Specialty> crudSpecialtyService, SpecialtiesPrintConsoleService printSpecialtyService)
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

        public async Task StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printSpecialtyService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            await _crudSpecialtyService.Create();
                            break;
                        case 2:
                            await _crudSpecialtyService.Delete();
                            break;
                        case 3:
                            await _crudSpecialtyService.Update();
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
