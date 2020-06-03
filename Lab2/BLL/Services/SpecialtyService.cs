using BLL.Models;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.DTO;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly IRepository<SpecialtyDTO> _specialtyRepository;
        private readonly IRepository<GroupDTO> _groupRepository;
        private readonly IRepository<StudentDTO> _studentRepository;
        private readonly IMapper _mapper;

        public SpecialtyService(IRepository<SpecialtyDTO> specialtyRepository,
                                IRepository<GroupDTO> groupRepository,
                                IRepository<StudentDTO> studentRepository,
                                IMapper mapper)
        {
            _specialtyRepository = specialtyRepository;
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task Create(Specialty specialty)
        {
            if (specialty == null)
            {
                throw new ValidationException("Data was not received");
            }

            var tempSpecialty = await GetSpecialtyByName(specialty.Name);

            //Checking that the specialty has a unique name
            if (tempSpecialty != null)
            {
                throw new ValidationException("Specialty with that name already exist");
            }

            var specialtyDTO = _mapper.Map<Specialty, SpecialtyDTO>(specialty);

            await _specialtyRepository.Create(specialtyDTO);
        }

        public async Task Delete(int id)
        {
            if (await _specialtyRepository.GetById(id) == null)
            {
                throw new ValidationException("Specialty was not found");
            }

            //Delete all groups and students from this specialty
            var groups = (await _groupRepository.GetAll()).Where(group => group.SpecialtyId == id).ToList();
            List<StudentDTO> students;
            foreach (var group in groups)
            {
                students = (await _studentRepository.GetAll()).Where(student => student.GroupId == group.Id).ToList();

                foreach (var student in students)
                {
                    await _studentRepository.Delete(student);
                }

                await _groupRepository.Delete(group);
            }

            await _specialtyRepository.Delete(await _specialtyRepository.GetById(id));
        }

        public async Task<List<Specialty>> GetAll()
        {
            var specialties = await _specialtyRepository.GetAll();
            return _mapper.Map<List<SpecialtyDTO>, List<Specialty>>(specialties);
        }

        public async Task<Specialty> GetById(int id)
        {
            var specialty = await _specialtyRepository.GetById(id);

            if (specialty == null)
            {
                throw new ValidationException("Specialty was not found");
            }

            return _mapper.Map<SpecialtyDTO, Specialty>(specialty);
        }

        public async Task Update(Specialty specialty)
        {
            if (specialty == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (await _specialtyRepository.GetById(specialty.Id) == null)
            {
                throw new ValidationException("Specialty was not found");
            }

            var tempSpecialty = await GetSpecialtyByName(specialty.Name);

            if (tempSpecialty != null && tempSpecialty.Id != specialty.Id)
            {
                throw new ValidationException("Specialty with that name already exist");
            }

            var specialtyDTO = _mapper.Map<Specialty, SpecialtyDTO>(specialty);

            await _specialtyRepository.Update(specialtyDTO);
        }

        public async Task<Specialty> GetSpecialtyByName(string name)
        {
            var resultingSpecialty = (await _specialtyRepository.GetAll()).FirstOrDefault(specialty => specialty.Name == name);

            return _mapper.Map<SpecialtyDTO, Specialty>(resultingSpecialty);
        }
    }
}
