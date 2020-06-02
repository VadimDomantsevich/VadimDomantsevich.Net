using BLL.Infrastructure;
using BLL.Models;
using System;
using UI.Interfaces;
using UI.View.PrintConsoleService;

namespace UI.View.MainConsoleService
{
    public class SubjectBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Subject> _crudSubjectService;
        private readonly IPrintConsoleService _printSubjectService;

        public SubjectBaseConsoleService(ICrudConsoleService<Subject> crudSubjectService, SubjectPrintConsoleService printSubjectService)
        {
            _crudSubjectService = crudSubjectService;
            _printSubjectService = printSubjectService;
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Add subject");
            Console.WriteLine("2. Delete subject");
            Console.WriteLine("3. Update subject");
            Console.WriteLine("4. Back");
        }

        public void StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    _printSubjectService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            _crudSubjectService.Create();
                            break;
                        case 2:
                            _crudSubjectService.Delete();
                            break;
                        case 3:
                            _crudSubjectService.Update();
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
