using BLL.Models;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.DTO;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace BLL.Services
{
    public class SpecialtyServices : ISpecialtyServices
    {
        private readonly IRepository<SpecialtyDTO> _specialtyRepository;
        private readonly IRepository<GroupDTO> _groupRepository;
        private readonly IRepository<StudentDTO> _studentRepository;
        private readonly IMapper _mapper;

        public SpecialtyServices(IRepository<SpecialtyDTO> specialtyRepository,
                                IRepository<GroupDTO> groupRepository,
                                IRepository<StudentDTO> studentRepository,
                                IMapper mapper)
        {
            _specialtyRepository = specialtyRepository;
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public void Create(Specialty specialty)
        {
            if (specialty == null)
            {
                throw new ValidationException("Data was not received");
            }

            var tempSpecialty = GetSpecialtyByName(specialty.Name);

            //Checking that the specialty has a unique name
            if (tempSpecialty != null)
            {
                throw new ValidationException("Specialty with that name already exist");
            }

            var specialtyDTO = _mapper.Map<Specialty, SpecialtyDTO>(specialty);

            _specialtyRepository.Create(specialtyDTO);
        }

        public void Delete(int id)
        {
            if (_specialtyRepository.GetById(id) == null)
            {
                throw new ValidationException("Specialty was not found");
            }

            //Delete all groups and students from this specialty
            var groups = _groupRepository.GetAll().Where(group => group.SpecialtyId == id);
            IQueryable<StudentDTO> students;
            foreach (var group in groups)
            {
                students = _studentRepository.GetAll().Where(student => student.GroupId == group.Id);

                foreach (var student in students)
                {
                    _studentRepository.Delete(student);
                }

                _groupRepository.Delete(group);
            }

            _specialtyRepository.Delete(_specialtyRepository.GetById(id));
        }

        public IEnumerable<Specialty> GetAll()
        {
            var specialties = _specialtyRepository.GetAll();
            return _mapper.Map<IEnumerable<SpecialtyDTO>, List<Specialty>>(specialties);
        }

        public Specialty GetById(int id)
        {
            var specialty = _specialtyRepository.GetById(id);

            if (specialty == null)
            {
                throw new ValidationException("Specialty was not found");
            }

            return _mapper.Map<SpecialtyDTO, Specialty>(specialty);
        }

        public void Update(Specialty specialty)
        {
            if (specialty == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (_specialtyRepository.GetById(specialty.Id) == null)
            {
                throw new ValidationException("Specialty was not found");
            }

            var tempSpecialty = GetSpecialtyByName(specialty.Name);

            if (tempSpecialty != null && tempSpecialty.Id != specialty.Id)
            {
                throw new ValidationException("Specialty with that name already exist");
            }

            var specialtyDTO = _mapper.Map<Specialty, SpecialtyDTO>(specialty);

            _specialtyRepository.Update(specialtyDTO);
        }

        public Specialty GetSpecialtyByName(string name)
        {
            var resultingSpecialty = _specialtyRepository.GetAll().FirstOrDefault(specialty => specialty.Name == name);

            return _mapper.Map<SpecialtyDTO, Specialty>(resultingSpecialty);
        }
    }
}
