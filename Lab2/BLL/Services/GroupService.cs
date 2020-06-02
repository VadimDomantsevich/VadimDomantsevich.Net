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
    public class GroupService : IGroupService
    {
        private readonly IRepository<GroupDTO> _groupRepository;
        private readonly IRepository<StudentDTO> _studentRepository;
        private readonly IMapper _mapper;

        public GroupService(IRepository<GroupDTO> groupRepository, IRepository<StudentDTO> studentRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;

            _mapper = mapper;
        }

        public async Task Create(Group group)
        {
            if (group == null)
            {
                throw new ValidationException("Data was not received");
            }

            var tempGroup = await GetGroupByName(group.Name);

            //Checking that the group has a unique name
            if (tempGroup != null)
            {
                throw new ValidationException("Group with that name already exist");
            }

            var groupDTO = _mapper.Map<Group, GroupDTO>(group);

            await _groupRepository.Create(groupDTO);
        }

        public async Task Delete(int id)
        {
            if (await _groupRepository.GetById(id) == null)
            {
                throw new ValidationException("Group was not found");
            }

            //Delete all students from this group
            var students = (await _studentRepository.GetAll()).Where(student => student.GroupId == id);

            foreach (var student in students)
            {
                await _studentRepository.Delete(student);
            }

            await _groupRepository.Delete(await _groupRepository.GetById(id));
        }

        public async Task<Group> GetById(int id)
        {
            var groupDTO = await _groupRepository.GetById(id);

            if (groupDTO == null)
            {
                throw new ValidationException("Group was not found");
            }

            return _mapper.Map<GroupDTO, Group>(groupDTO);
        }

        public async Task<List<Group>> GetAll()
        {
            var groups = await _groupRepository.GetAll();
            return _mapper.Map<List<GroupDTO>, List<Group>>(groups);
        }

        public async Task<List<Group>> GetGroupsBySpecialtyId(int specialtyId)
        {
            var groups = (await _groupRepository.GetAll()).Where(group => group.SpecialtyId == specialtyId).ToList();

            return _mapper.Map<List<GroupDTO>, List<Group>>(groups);
        }

        public async Task Update(Group group)
        {
            if (group == null)
            {
                throw new ValidationException("Data was not received");
            }

            if (await _groupRepository.GetById(group.Id) == null)
            {
                throw new ValidationException("Group was not found");
            }
            
            var tempGroup = await GetGroupByName(group.Name);

            if (tempGroup != null && tempGroup.Id != group.Id)
            {
                throw new ValidationException("Group with that name already exist");
            }

            var groupDTO = _mapper.Map<Group, GroupDTO>(group);

            await _groupRepository.Update(groupDTO);
        }

        public async Task<Group> GetGroupByName(string name)
        {
            var resultingGroup = (await _groupRepository.GetAll()).FirstOrDefault(group => group.Name == name);

            return _mapper.Map<GroupDTO, Group>(resultingGroup);
        }
    }
}
