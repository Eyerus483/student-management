using AutoMapper;
using student_management.Dto.StudentDto;
using student_management.Model;

namespace student_management
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddStudentDto, Student>();
        }
    }
}