using AutoMapper;
using Student_Management.Models;
using Student_Management.Models.Dtos;
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
