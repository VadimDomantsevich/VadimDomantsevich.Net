using BLL.Models;
using BLL.Services;
using System;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
{
    public class SpecialtiesCrudConsoleService : ICrudConsoleService<Specialty>
    {
        private readonly SpecialtyService _specialtyService;

        public SpecialtiesCrudConsoleService(SpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        public async Task Create()
        {
            var specialty = await CreateModel();

            await _specialtyService.Create(specialty);
        }

        public async Task<Specialty> CreateModel()
        {
            return await Task.Run(() => 
            {
                Console.WriteLine("Enter specialty name:");
                var name = Console.ReadLine();

                var specialty = new Specialty
                {
                    Name = name
                };

                return specialty;
            });
        }

        public async Task Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            await _specialtyService.Delete(id);
        }

        public async Task Update()
        {
            Console.WriteLine("Enter Id of specialty to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var specialty = await CreateModel();
            specialty.Id = id;

            await _specialtyService.Update(specialty);
        }
    }
}
