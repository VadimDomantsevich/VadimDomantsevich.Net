using BLL.Infrastructure;
using BLL.Models;
using System;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
{
    public class GroupsBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Group> _crudGroupService;
        private readonly IPrintConsoleService _printGroupService;

        public GroupsBaseConsoleService(ICrudConsoleService<Group> crudGroupService, GroupsPrintConsoleService printGroupService)
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

        public async Task StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printGroupService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            await _crudGroupService.Create();
                            break;
                        case 2:
                            await _crudGroupService.Delete();
                            break;
                        case 3:
                            await _crudGroupService.Update();
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
