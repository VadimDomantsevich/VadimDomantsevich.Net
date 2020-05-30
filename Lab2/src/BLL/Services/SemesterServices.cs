using BLL.Models;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.DTO;
using System.Collections.Generic;
using AutoMapper;

namespace BLL.Services
{
    public class SemesterServices : IServices<Semester>
    {
        private readonly IRepository<SemesterDTO> _semesterRepository;
        private readonly IMapper _mapper;

        public SemesterServices(IRepository<SemesterDTO> semesterRepository, IMapper mapper)
        {
            _semesterRepository = semesterRepository;
            _mapper = mapper;
        }

        public void Create(Semester semester)
        {
            if (semester == null)
            {
                throw new ValidationException("Data was not received");
            }

            var semesterDTO = _mapper.Map<Semester, SemesterDTO>(semester);

            _semesterRepository.Create(semesterDTO);
        }

        public void Delete(int id)
        {
            if (_semesterRepository.GetById(id) == null)
            {
                throw new ValidationException("Semester was not found");
            }

            _semesterRepository.Delete(_semesterRepository.GetById(id));
        }

        public Semester GetById(int id)
        {
            var semesterDTO = _semesterRepository.GetById(id);

            if (semesterDTO == null)
            {
                throw new ValidationException("Semester was not found");
            }

            return _mapper.Map<SemesterDTO, Semester>(semesterDTO);
        }

        public IEnumerable<Semester> GetAll()
        {
            var semesters = _semesterRepository.GetAll();
            return _mapper.Map<IEnumerable<SemesterDTO>, List<Semester>>(semesters);
        }

        public void Update(Semester semester)
        {
            if (semester == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (_semesterRepository.GetById(semester.Id) == null)
            {
                throw new ValidationException("Semester was not found");
            }

            var semesterDTO = _mapper.Map<Semester, SemesterDTO>(semester);

            _semesterRepository.Update(semesterDTO);
        }
    }
}
