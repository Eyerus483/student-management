using AutoMapper;
using student_management.Dto.AdminDto;
using student_management.Dto.CourseDto;
using student_management.Dto.DepartmentDto;
using student_management.Dto.StudentDto;
using student_management.Dto.TeacherDto;
using student_management.Model;

namespace student_management
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudentRequestDto, Student>();
            CreateMap<Admin, AdminResponseDto> ();
            CreateMap<Student, StudentResponseDto> ();
            CreateMap<Admin, AdminProfileResponseDto> ();
            CreateMap<Student, StudentProfileResponseDto> ();
            CreateMap<TeacherRequestDto, Teacher>();
            CreateMap<DepartmentRequestDto, Department>();
            CreateMap<Department, DepartmentResponseDto>();
            CreateMap<Teacher, TeacherResponseDto>();
            CreateMap<CourseRequestDto, Course>();
            CreateMap<Course, CourseResponseDto>();
            CreateMap<AdminUpdateDto, Admin>();
            CreateMap<CourseResponseDto, Course>();
            CreateMap<DepartmentUpdateDto, Department>();
            CreateMap<Department, DepartmentResponseDto>();

            
        }
    }
}