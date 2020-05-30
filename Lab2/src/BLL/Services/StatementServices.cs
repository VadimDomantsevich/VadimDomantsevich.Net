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
    public class StatementServices : IStatementServices
    {
        private readonly IRepository<StatementDTO> _statementRepository;
        private readonly IMapper _mapper;

        public StatementServices(
            IRepository<StatementDTO> statementRepository,
            IMapper mapper)
        {
            _statementRepository = statementRepository;
            _mapper = mapper;
        }

        public void Create(Statement statement)
        {
            if (statement == null)
            {
                throw new ValidationException("Data was not received");
            }

            var statementDTO = _mapper.Map<Statement, StatementDTO>(statement);

            _statementRepository.Create(statementDTO);
        }

        public void Delete(int id)
        {
            if (_statementRepository.GetById(id) == null)
            {
                throw new ValidationException("Statement was not found");
            }

            _statementRepository.Delete(_statementRepository.GetById(id));
        }

        public Statement GetById(int id)
        {
            var statement = _statementRepository.GetById(id);

            if (statement == null)
            {
                throw new ValidationException("Statement was not found");
            }

            return _mapper.Map<StatementDTO, Statement>(statement);
        }

        public IEnumerable<Statement> GetAll()
        {
            var statements = _statementRepository.GetAll();

            return _mapper.Map<IEnumerable<StatementDTO>, List<Statement>>(statements);
        }

        public IEnumerable<Statement> GetStatementsByStudentId(int studentId)
        {
            var statements = _statementRepository.GetAll().Where(statement => statement.StudentId == studentId);

            return _mapper.Map<IEnumerable<StatementDTO>, List<Statement>>(statements);
        }

        public IEnumerable<Statement> GetStatementsBySubjectId(int subjectId)
        {
            var statements = _statementRepository.GetAll().Where(statement => statement.SubjectId == subjectId);

            return _mapper.Map<IEnumerable<StatementDTO>, List<Statement>>(statements);
        }

        public IEnumerable<Statement> GetStatementsBySemesterId(int semesterId)
        {
            var statements = _statementRepository.GetAll().Where(statement => statement.SemesterId == semesterId);

            return _mapper.Map<IEnumerable<StatementDTO>, List<Statement>>(statements);
        }

        public void Update(Statement statement)
        {
            if (statement == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (_statementRepository.GetById(statement.Id) == null)
            {
                throw new ValidationException("Statement was not found");
            }

            var statementDTO = _mapper.Map<Statement, StatementDTO>(statement);

            _statementRepository.Update(statementDTO);
        }
    }
}
