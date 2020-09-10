using AutoMapper;
using Student_Management.Models;
using Student_Management.Models.Dtos.DepartmentDto;
using Student_Management.Models.Dtos.StudentDto;
using Student_Management.Models.Dtos.TeacherDto;

namespace Student_Management.AutoMapper
{
    public class ManagementMapper : Profile
    {
        public ManagementMapper()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, DepartmentCreateDto>().ReverseMap();
            CreateMap<Department, DepartmentUpdateDto>().ReverseMap();

            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Teacher, TeacherCreateDto>().ReverseMap();
            CreateMap<Teacher, TeacherUpdateDto>().ReverseMap();

            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Student, StudentCreateDto>().ReverseMap();
            CreateMap<Student, StudentUpdateDto>().ReverseMap();
        }
    }
}