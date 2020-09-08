﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management.Models;
using Student_Management.Models.Dtos;
using Student_Management.Repository.IRepository;

namespace Student_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepo, IMapper mapper)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentRepo.GetStudents();
            var studentDto = new List<StudentDto>();

            foreach(var obj in students)
            {
                studentDto.Add(_mapper.Map<StudentDto>(obj));
            }

            return Ok(studentDto);
        }

        [HttpGet("{studentId:int}", Name = nameof(GetStudent))] //{uniqueName:type}
        public IActionResult GetStudent(int studentId)
        {
            var student = _studentRepo.GetStudent(studentId);

            if(student == null)
            {
                return NotFound();
            }

            var studentDto = _mapper.Map<StudentDto>(student);
            return Ok(studentDto);
        }

        [HttpPost]
        public IActionResult CreateStudent(StudentDto studentDto)
        {
            if(studentDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_studentRepo.isStudentExists(studentDto.Name))
            {
                ModelState.AddModelError("", $"Teacher is exists!");
                return StatusCode(404, ModelState);
            }

            var studentObj = _mapper.Map<Student>(studentDto);

            if (!_studentRepo.CreateStudent(studentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {studentObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute(nameof(GetStudent), new { studentId = studentObj.Id }, studentObj);
        }

        [HttpPatch]
        public IActionResult UpdateStudent(int studentId, StudentDto studentDto)
        {
            if(studentDto == null || studentId != studentDto.Id)
            {
                return BadRequest(ModelState);
            }

            var studentObj = _mapper.Map<Student>(studentDto);

            if (!_studentRepo.UpdateStudent(studentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {studentObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int studentId)
        {
            if (!_studentRepo.isStudentExists(studentId))
            {
                return NotFound();
            }

            var studentObj = _studentRepo.GetStudent(studentId);

            if (!_studentRepo.DeleteStudent(studentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {studentObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
