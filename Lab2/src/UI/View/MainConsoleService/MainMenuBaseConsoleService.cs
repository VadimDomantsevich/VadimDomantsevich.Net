using System;
using UI.Interfaces;

namespace UI.View.MainConsoleService
{
    public class MainMenuBaseConsoleService : IConsoleService
    {
        private readonly GroupBaseConsoleService _groupConsoleService;
        private readonly SemesterBaseConsoleService _semesterConsoleService;
        private readonly SpecialtyBaseConsoleService _specialtyConsoleService;
        private readonly StudentBaseConsoleService _studentConsoleService;
        private readonly SubjectBaseConsoleService _subjectConsoleService;
        private readonly StatementBaseConsoleService _statementConsoleService;

        public MainMenuBaseConsoleService(
            GroupBaseConsoleService groupConsoleService,
            SemesterBaseConsoleService semesterConsoleService,
            SpecialtyBaseConsoleService specialtyConsoleService,
            StudentBaseConsoleService studentConsoleService,
            SubjectBaseConsoleService subjectConsoleService,
            StatementBaseConsoleService statementConsoleService)
        {
            _groupConsoleService = groupConsoleService;
            _semesterConsoleService = semesterConsoleService;
            _specialtyConsoleService = specialtyConsoleService;
            _studentConsoleService = studentConsoleService;
            _subjectConsoleService = subjectConsoleService;
            _statementConsoleService = statementConsoleService;
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Groups");
            Console.WriteLine("2. Semesters");
            Console.WriteLine("3. Specialities");
            Console.WriteLine("4. Students");
            Console.WriteLine("5. Subjects");
            Console.WriteLine("6. Statements");
            Console.WriteLine("7. Exit");
        }

        public void StartLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    PrintMenu();
                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            {
                                _groupConsoleService.StartLoop();
                            }
                            break;
                        case 2:
                            {
                                _semesterConsoleService.StartLoop();
                            }
                            break;
                        case 3:
                            {
                                _specialtyConsoleService.StartLoop();
                            }
                            break;
                        case 4:
                            {
                                _studentConsoleService.StartLoop();
                            }
                            break;
                        case 5:
                            {
                                _subjectConsoleService.StartLoop();
                            }
                            break;
                        case 6:
                            {
                                _statementConsoleService.StartLoop();
                            }
                            break;
                        case 7:
                            {
                                return;
                            }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
            }
        }
    }
}
