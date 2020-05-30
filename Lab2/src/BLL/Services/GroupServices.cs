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
    public class GroupServices : IGroupServices
    {
        private readonly IRepository<GroupDTO> _groupRepository;
        private readonly IRepository<StudentDTO> _studentRepository;
        private readonly IMapper _mapper;

        public GroupServices(IRepository<GroupDTO> groupRepository, IRepository<StudentDTO> studentRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;

            _mapper = mapper;
        }

        public void Create(Group group)
        {
            if (group == null)
            {
                throw new ValidationException("Data was not received");
            }

            var tempGroup = GetGroupByName(group.Name);

            //Checking that the group has a unique name
            if (tempGroup != null)
            {
                throw new ValidationException("Group with that name already exist");
            }

            var groupDTO = _mapper.Map<Group, GroupDTO>(group);

            _groupRepository.Create(groupDTO);
        }

        public void Delete(int id)
        {
            if (_groupRepository.GetById(id) == null)
            {
                throw new ValidationException("Group was not found");
            }

            //Delete all students from this group
            var students = _studentRepository.GetAll().Where(student => student.GroupId == id);

            foreach (var student in students)
            {
                _studentRepository.Delete(student);
            }

            _groupRepository.Delete(_groupRepository.GetById(id));
        }

        public Group GetById(int id)
        {
            var groupDTO = _groupRepository.GetById(id);

            if (groupDTO == null)
            {
                throw new ValidationException("Group was not found");
            }

            return _mapper.Map<GroupDTO, Group>(groupDTO);
        }

        public IEnumerable<Group> GetAll()
        {
            var groups = _groupRepository.GetAll();
            return _mapper.Map<IEnumerable<GroupDTO>, List<Group>>(groups);
        }

        public IEnumerable<Group> GetGroupsBySpecialtyId(int specialtyId)
        {
            var groups = _groupRepository.GetAll().Where(group => group.SpecialtyId == specialtyId);

            return _mapper.Map<IEnumerable<GroupDTO>, List<Group>>(groups);
        }

        public void Update(Group group)
        {
            if (group == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (_groupRepository.GetById(group.Id) == null)
            {
                throw new ValidationException("Group was not found");
            }
            
            var tempGroup = GetGroupByName(group.Name);

            if (tempGroup != null && tempGroup.Id != group.Id)
            {
                throw new ValidationException("Group with that name already exist");
            }

            var groupDTO = _mapper.Map<Group, GroupDTO>(group);

            _groupRepository.Update(groupDTO);
        }

        public Group GetGroupByName(string name)
        {
            var resultingGroup = _groupRepository.GetAll().FirstOrDefault(group => group.Name == name);

            return _mapper.Map<GroupDTO, Group>(resultingGroup);
        }
    }
}
