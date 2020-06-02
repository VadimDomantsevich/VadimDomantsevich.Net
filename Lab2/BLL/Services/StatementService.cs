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
    public class StatementService : IStatementService
    {
        private readonly IRepository<StatementDTO> _statementRepository;
        private readonly IMapper _mapper;

        public StatementService(
            IRepository<StatementDTO> statementRepository,
            IMapper mapper)
        {
            _statementRepository = statementRepository;
            _mapper = mapper;
        }

        public async Task Create(Statement statement)
        {
            if (statement == null)
            {
                throw new ValidationException("Data was not received");
            }

            var statementDTO = _mapper.Map<Statement, StatementDTO>(statement);

            await _statementRepository.Create(statementDTO);
        }

        public async Task Delete(int id)
        {
            if (await _statementRepository.GetById(id) == null)
            {
                throw new ValidationException("Statement was not found");
            }

            await _statementRepository.Delete(await _statementRepository.GetById(id));
        }

        public async Task<Statement> GetById(int id)
        {
            var statement = await _statementRepository.GetById(id);

            if (statement == null)
            {
                throw new ValidationException("Statement was not found");
            }

            return _mapper.Map<StatementDTO, Statement>(statement);
        }

        public async Task<IEnumerable<Statement>> GetAll()
        {
            var statements = await _statementRepository.GetAll();

            return _mapper.Map<IEnumerable<StatementDTO>, List<Statement>>(statements);
        }

        public async Task<IEnumerable<Statement>> GetStatementsByStudentId(int studentId)
        {
            var statements = (await _statementRepository.GetAll()).Where(statement => statement.StudentId == studentId);

            return _mapper.Map<IEnumerable<StatementDTO>, List<Statement>>(statements);
        }

        public async Task<IEnumerable<Statement>> GetStatementsBySubjectId(int subjectId)
        {
            var statements = (await _statementRepository.GetAll()).Where(statement => statement.SubjectId == subjectId);

            return _mapper.Map<IEnumerable<StatementDTO>, List<Statement>>(statements);
        }

        public async Task<IEnumerable<Statement>> GetStatementsBySemesterId(int semesterId)
        {
            var statements = (await _statementRepository.GetAll()).Where(statement => statement.SemesterId == semesterId);

            return _mapper.Map<IEnumerable<StatementDTO>, List<Statement>>(statements);
        }

        public async Task Update(Statement statement)
        {
            if (statement == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (await _statementRepository.GetById(statement.Id) == null)
            {
                throw new ValidationException("Statement was not found");
            }

            var statementDTO = _mapper.Map<Statement, StatementDTO>(statement);

            await _statementRepository.Update(statementDTO);
        }
    }
}
