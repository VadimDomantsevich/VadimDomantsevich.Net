using System;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
{
    public class MainMenuBaseConsoleService : IConsoleService
    {
        private readonly GroupsBaseConsoleService _groupConsoleService;
        private readonly SemestersBaseConsoleService _semesterConsoleService;
        private readonly SpecialtiesBaseConsoleService _specialtyConsoleService;
        private readonly StudentsBaseConsoleService _studentConsoleService;
        private readonly SubjectsBaseConsoleService _subjectConsoleService;
        private readonly StatementsBaseConsoleService _statementConsoleService;

        public MainMenuBaseConsoleService(
            GroupsBaseConsoleService groupConsoleService,
            SemestersBaseConsoleService semesterConsoleService,
            SpecialtiesBaseConsoleService specialtyConsoleService,
            StudentsBaseConsoleService studentConsoleService,
            SubjectsBaseConsoleService subjectConsoleService,
            StatementsBaseConsoleService statementConsoleService)
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

        public async Task StartLoop()
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
                                await _groupConsoleService.StartLoop();
                            }
                            break;
                        case 2:
                            {
                                await _semesterConsoleService.StartLoop();
                            }
                            break;
                        case 3:
                            {
                                await _specialtyConsoleService.StartLoop();
                            }
                            break;
                        case 4:
                            {
                                await _studentConsoleService.StartLoop();
                            }
                            break;
                        case 5:
                            {
                                await _subjectConsoleService.StartLoop();
                            }
                            break;
                        case 6:
                            {
                                await _statementConsoleService.StartLoop();
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
