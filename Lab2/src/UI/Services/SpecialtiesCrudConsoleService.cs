using BLL.Models;
using BLL.Services;
using System;
using UI.Interfaces;

namespace UI.View.CrudConsoleService
{
    public class SpecialtyCrudConsoleService : ICrudConsoleService<Specialty>
    {
        private readonly SpecialtyServices _specialtyService;

        public SpecialtyCrudConsoleService(SpecialtyServices specialtyService)
        {
            _specialtyService = specialtyService;
        }

        public void Create()
        {
            var specialty = CreateModel();

            _specialtyService.Create(specialty);
        }

        public Specialty CreateModel()
        {
            Console.WriteLine("Enter specialty name:");
            var name = Console.ReadLine();

            var specialty = new Specialty
            {
                Name = name
            };

            return specialty;
        }

        public void Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            _specialtyService.Delete(id);
        }

        public void Update()
        {
            Console.WriteLine("Enter Id of specialty to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var specialty = CreateModel();
            specialty.Id = id;

            _specialtyService.Update(specialty);
        }
    }
}
