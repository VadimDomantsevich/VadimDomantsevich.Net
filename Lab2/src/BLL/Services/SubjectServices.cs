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
    public class SubjectServices : ISubjectServices
    {
        private readonly IRepository<SubjectDTO> _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectServices(IRepository<SubjectDTO> subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public void Create(Subject subject)
        {
            if (subject == null)
            {
                throw new ValidationException("Data was not received");
            }

            var tempSubject = GetSubjectByName(subject.Name);

            //Checking that the specialty has a unique name
            if (tempSubject != null)
            {
                throw new ValidationException("Subject with that name already exist");
            }

            var subjectDTO = _mapper.Map<Subject, SubjectDTO>(subject);

            _subjectRepository.Create(subjectDTO);
        }

        public void Delete(int id)
        {
            if (_subjectRepository.GetById(id) == null)
            {
                throw new ValidationException("Subject was not found");
            }

            _subjectRepository.Delete(_subjectRepository.GetById(id));
        }

        public Subject GetById(int id)
        {
            var subject = _subjectRepository.GetById(id);

            if (subject == null)
            {
                throw new ValidationException("Subject was not found");
            }

            return _mapper.Map<SubjectDTO, Subject>(subject);
        }

        public IEnumerable<Subject> GetAll()
        {
            var subjects = _subjectRepository.GetAll();
            return _mapper.Map<IEnumerable<SubjectDTO>, List<Subject>>(subjects);
        }

        public void Update(Subject subject)
        {
            if (subject == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (_subjectRepository.GetById(subject.Id) == null)
            {
                throw new ValidationException("Subject was not found");
            }

            var tempSubject = GetSubjectByName(subject.Name);

            if (tempSubject != null && tempSubject.Id != subject.Id)
            {
                throw new ValidationException("Subject with that name already exist");
            }

            var subjectDTO = _mapper.Map<Subject, SubjectDTO>(subject);

            _subjectRepository.Update(subjectDTO);
        }

        public Subject GetSubjectByName(string name)
        {
            var resultingSubject = _subjectRepository.GetAll().FirstOrDefault(subject => subject.Name == name);

            return _mapper.Map<SubjectDTO, Subject>(resultingSubject);
        }
    }
}
