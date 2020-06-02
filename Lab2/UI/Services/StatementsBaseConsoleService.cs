using BLL.Infrastructure;
using BLL.Models;
using System;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
{
    public class StatementsBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Statement> _crudStatementService;
        private readonly IPrintConsoleService _printStatementService;

        public StatementsBaseConsoleService(ICrudConsoleService<Statement> crudStatementService, StatementsPrintConsoleService printStatementService)
        {
            _crudStatementService = crudStatementService;
            _printStatementService = printStatementService;
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Add statement");
            Console.WriteLine("2. Delete statement");
            Console.WriteLine("3. Update statement");
            Console.WriteLine("4. Back");
        }

        public async Task StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printStatementService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            await _crudStatementService.Create();
                            break;
                        case 2:
                            await _crudStatementService.Delete();
                            break;
                        case 3:
                            await _crudStatementService.Update();
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
