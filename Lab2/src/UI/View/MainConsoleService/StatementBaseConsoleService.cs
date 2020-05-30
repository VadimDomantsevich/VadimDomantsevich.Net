using BLL.Infrastructure;
using BLL.Models;
using System;
using UI.Interfaces;
using UI.View.PrintConsoleService;

namespace UI.View.MainConsoleService
{
    public class StatementBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Statement> _crudStatementService;
        private readonly IPrintConsoleService _printStatementService;

        public StatementBaseConsoleService(ICrudConsoleService<Statement> crudStatementService, StatementPrintConsoleService printStatementService)
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

        public void StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    _printStatementService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            _crudStatementService.Create();
                            break;
                        case 2:
                            _crudStatementService.Delete();
                            break;
                        case 3:
                            _crudStatementService.Update();
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
