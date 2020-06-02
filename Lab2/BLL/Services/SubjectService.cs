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
    public class SubjectService : ISubjectService
    {
        private readonly IRepository<SubjectDTO> _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectService(IRepository<SubjectDTO> subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task Create(Subject subject)
        {
            if (subject == null)
            {
                throw new ValidationException("Data was not received");
            }

            var tempSubject = await GetSubjectByName(subject.Name);

            //Checking that the specialty has a unique name
            if (tempSubject != null)
            {
                throw new ValidationException("Subject with that name already exist");
            }

            var subjectDTO = _mapper.Map<Subject, SubjectDTO>(subject);

            await _subjectRepository.Create(subjectDTO);
        }

        public async Task Delete(int id)
        {
            if (await _subjectRepository.GetById(id) == null)
            {
                throw new ValidationException("Subject was not found");
            }

            await _subjectRepository.Delete(await _subjectRepository.GetById(id));
        }

        public async Task<Subject> GetById(int id)
        {
            var subject = await _subjectRepository.GetById(id);

            if (subject == null)
            {
                throw new ValidationException("Subject was not found");
            }

            return _mapper.Map<SubjectDTO, Subject>(subject);
        }

        public async Task<List<Subject>> GetAll()
        {
            var subjects = await _subjectRepository.GetAll();
            return _mapper.Map<List<SubjectDTO>, List<Subject>>(subjects);
        }

        public async Task Update(Subject subject)
        {
            if (subject == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (await _subjectRepository.GetById(subject.Id) == null)
            {
                throw new ValidationException("Subject was not found");
            }

            var tempSubject = await GetSubjectByName(subject.Name);

            if (tempSubject != null && tempSubject.Id != subject.Id)
            {
                throw new ValidationException("Subject with that name already exist");
            }

            var subjectDTO = _mapper.Map<Subject, SubjectDTO>(subject);

            await _subjectRepository.Update(subjectDTO);
        }

        public async Task<Subject> GetSubjectByName(string name)
        {
            var resultingSubject = (await _subjectRepository.GetAll()).FirstOrDefault(subject => subject.Name == name);

            return _mapper.Map<SubjectDTO, Subject>(resultingSubject);
        }
    }
}
