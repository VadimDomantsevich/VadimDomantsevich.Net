using BLL.Models;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.DTO;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SemesterService : IService<Semester>
    {
        private readonly IRepository<SemesterDTO> _semesterRepository;
        private readonly IMapper _mapper;

        public SemesterService(IRepository<SemesterDTO> semesterRepository, IMapper mapper)
        {
            _semesterRepository = semesterRepository;
            _mapper = mapper;
        }

        public async Task Create(Semester semester)
        {
            if (semester == null)
            {
                throw new ValidationException("Data was not received");
            }

            var semesterDTO = _mapper.Map<Semester, SemesterDTO>(semester);

            await _semesterRepository.Create(semesterDTO);
        }

        public async Task Delete(int id)
        {
            if (await _semesterRepository.GetById(id) == null)
            {
                throw new ValidationException("Semester was not found");
            }

            await _semesterRepository.Delete(await _semesterRepository.GetById(id));
        }

        public async Task<Semester> GetById(int id)
        {
            var semesterDTO = await _semesterRepository.GetById(id);

            if (semesterDTO == null)
            {
                throw new ValidationException("Semester was not found");
            }

            return _mapper.Map<SemesterDTO, Semester>(semesterDTO);
        }

        public async Task<List<Semester>> GetAll()
        {
            var semesters = await _semesterRepository.GetAll();
            return _mapper.Map<List<SemesterDTO>, List<Semester>>(semesters);
        }

        public async Task Update(Semester semester)
        {
            if (semester == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (await _semesterRepository.GetById(semester.Id) == null)
            {
                throw new ValidationException("Semester was not found");
            }

            var semesterDTO = _mapper.Map<Semester, SemesterDTO>(semester);

            await _semesterRepository.Update(semesterDTO);
        }
    }
}
