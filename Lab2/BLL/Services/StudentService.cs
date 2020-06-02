using BLL.Models;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<StudentDTO> _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IRepository<StudentDTO> studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task Create(Student student)
        {
            if (student == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (!Regex.Match(student.PhoneNumber, "^(\\+375|80)(29|25|44|33)(\\d{3})(\\d{2})(\\d{2})$").Success)
            {
                throw new ValidationException("Telephone number isn't valid");
            }

            if (!Regex.Match(student.RecordNumber, "^\\d{6}$").Success)
            {
                throw new ValidationException("Record number isn't valid");
            }

            var tempStudent = await GetStudentByRecordNumber(student.RecordNumber);

            //Checking that the student has a unique record and phone number
            if (tempStudent != null)
            {
                throw new ValidationException("Student with that record number already exist");
            }

            tempStudent = await GetStudentByPhoneNumber(student.PhoneNumber);

            if (tempStudent != null)
            {
                throw new ValidationException("Student with that phone number already exist");
            }

            var studentDTO = _mapper.Map<Student, StudentDTO>(student);

            await _studentRepository.Create(studentDTO);
        }

        public async Task Delete(int id)
        {
            if (await _studentRepository.GetById(id) == null)
            {
                throw new ValidationException("Student was not found");
            }

            await _studentRepository.Delete(await _studentRepository.GetById(id));
        }

        public async Task<Student> GetById(int id)
        {
            var student = await _studentRepository.GetById(id);

            if (student == null)
            {
                throw new ValidationException("Student was not found");
            }

            return _mapper.Map<StudentDTO, Student>(student);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var students = await _studentRepository.GetAll();

            return _mapper.Map<IEnumerable<StudentDTO>, List<Student>>(students);
        }

        public async Task<IEnumerable<Student>> GetStudentsByGroupId(int groupId)
        {
            var students = (await _studentRepository.GetAll()).Where(student => student.GroupId == groupId);

            return _mapper.Map<IEnumerable<StudentDTO>, List<Student>>(students);
        }

        public async Task Update(Student student)
        {
            if (student == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (await _studentRepository.GetById(student.Id) == null)
            {
                throw new ValidationException("Student was not found");
            }

            var tempStudent = await GetStudentByRecordNumber(student.RecordNumber);

            if (tempStudent != null && tempStudent.Id != student.Id)
            {
                throw new ValidationException("Student with that record number already exist");
            }

            tempStudent = await GetStudentByPhoneNumber(student.PhoneNumber);

            if (tempStudent != null && tempStudent.Id != student.Id)
            {
                throw new ValidationException("Student with that phone number already exist");
            }

            var studentDTO = _mapper.Map<Student, StudentDTO>(student);

            await _studentRepository.Update(studentDTO);
        }

        public async Task<Student> GetStudentByRecordNumber(string recordNumber)
        {
            var resultingStudent = (await _studentRepository.GetAll()).FirstOrDefault(student => student.RecordNumber == recordNumber);

            return _mapper.Map<StudentDTO, Student>(resultingStudent);
        }

        public async Task<Student> GetStudentByPhoneNumber(string phoneNumber)
        {
            var resultingStudent = (await _studentRepository.GetAll()).FirstOrDefault(student => student.PhoneNumber == phoneNumber);

            return _mapper.Map<StudentDTO, Student>(resultingStudent);
        }
    }
}
