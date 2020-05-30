using BLL.Infrastructure;
using BLL.Models;
using System;
using UI.Interfaces;
using UI.View.PrintConsoleService;

namespace UI.View.MainConsoleService
{
    public class GroupBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Group> _crudGroupService;
        private readonly IPrintConsoleService _printGroupService;

        public GroupBaseConsoleService(ICrudConsoleService<Group> crudGroupService, GroupPrintConsoleService printGroupService)
        {
            _crudGroupService = crudGroupService;
            _printGroupService = printGroupService;
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Add group");
            Console.WriteLine("2. Delete group");
            Console.WriteLine("3. Update group");
            Console.WriteLine("4. Back");
        }

        public void StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    _printGroupService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            _crudGroupService.Create();
                            break;
                        case 2:
                            _crudGroupService.Delete();
                            break;
                        case 3:
                            _crudGroupService.Update();
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
