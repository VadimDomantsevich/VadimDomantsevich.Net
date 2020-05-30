using BLL.Models;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;

namespace BLL.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IRepository<StudentDTO> _studentRepository;
        private readonly IMapper _mapper;

        public StudentServices(IRepository<StudentDTO> studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public void Create(Student student)
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

            var tempStudent = GetStudentByRecordNumber(student.RecordNumber);

            //Checking that the student has a unique record and phone number
            if (tempStudent != null)
            {
                throw new ValidationException("Student with that record number already exist");
            }

            tempStudent = GetStudentByPhoneNumber(student.PhoneNumber);

            if (tempStudent != null)
            {
                throw new ValidationException("Student with that phone number already exist");
            }

            var studentDTO = _mapper.Map<Student, StudentDTO>(student);

            _studentRepository.Create(studentDTO);
        }

        public void Delete(int id)
        {
            if (_studentRepository.GetById(id) == null)
            {
                throw new ValidationException("Student was not found");
            }

            _studentRepository.Delete(_studentRepository.GetById(id));
        }

        public Student GetById(int id)
        {
            var student = _studentRepository.GetById(id);

            if (student == null)
            {
                throw new ValidationException("Student was not found");
            }

            return _mapper.Map<StudentDTO, Student>(student);
        }

        public IEnumerable<Student> GetAll()
        {
            var students = _studentRepository.GetAll();

            return _mapper.Map<IEnumerable<StudentDTO>, List<Student>>(students);
        }

        public IEnumerable<Student> GetStudentsByGroupId(int groupId)
        {
            var students = _studentRepository.GetAll().Where(student => student.GroupId == groupId);

            return _mapper.Map<IEnumerable<StudentDTO>, List<Student>>(students);
        }

        public void Update(Student student)
        {
            if (student == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (_studentRepository.GetById(student.Id) == null)
            {
                throw new ValidationException("Student was not found");
            }

            var tempStudent = GetStudentByRecordNumber(student.RecordNumber);

            if (tempStudent != null && tempStudent.Id != student.Id)
            {
                throw new ValidationException("Student with that record number already exist");
            }

            tempStudent = GetStudentByPhoneNumber(student.PhoneNumber);

            if (tempStudent != null && tempStudent.Id != student.Id)
            {
                throw new ValidationException("Student with that phone number already exist");
            }

            var studentDTO = _mapper.Map<Student, StudentDTO>(student);

            _studentRepository.Update(studentDTO);
        }

        public Student GetStudentByRecordNumber(string recordNumber)
        {
            var resultingStudent = _studentRepository.GetAll().FirstOrDefault(student => student.RecordNumber == recordNumber);

            return _mapper.Map<StudentDTO, Student>(resultingStudent);
        }

        public Student GetStudentByPhoneNumber(string phoneNumber)
        {
            var resultingStudent = _studentRepository.GetAll().FirstOrDefault(student => student.PhoneNumber == phoneNumber);

            return _mapper.Map<StudentDTO, Student>(resultingStudent);
        }
    }
}
