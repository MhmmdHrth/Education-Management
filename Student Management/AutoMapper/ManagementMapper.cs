using AutoMapper;
using Student_Management.Models;
using Student_Management.Models.Dtos.DepartmentDto;
using Student_Management.Models.Dtos.StudentDto;
using Student_Management.Models.Dtos.TeacherDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.AutoMapper
{
    public class ManagementMapper : Profile
    {
        public ManagementMapper()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
