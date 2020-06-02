using AutoMapper;
using BLL.Models;
using DAL.DTO;

namespace BLL.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GroupDTO, Group>().ReverseMap();
            CreateMap<SemesterDTO, Semester>().ReverseMap();
            CreateMap<SpecialtyDTO, Specialty>().ReverseMap();
            CreateMap<StatementDTO, Statement>().ReverseMap();
            CreateMap<StudentDTO, Student>().ReverseMap();
            CreateMap<SubjectDTO, Subject>().ReverseMap();
        }
    }
}
