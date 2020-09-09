using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management.Models;
using Student_Management.Models.Dtos.StudentDto;
using Student_Management.Repository.IRepository;

namespace Student_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepo, IMapper mapper)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of all Students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<StudentDto>))]
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

        /// <summary>
        /// Get list of individual Student
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet("{studentId:int}", Name = nameof(GetStudent))] //{uniqueName:type}
        [ProducesResponseType(200, Type = typeof(StudentDto))]
        [ProducesResponseType(404)]
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

        /// <summary>
        /// Create Student
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(StudentDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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


        /// <summary>
        /// Update Student Details
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(204, Type = typeof(StudentDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
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
