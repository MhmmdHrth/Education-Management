using System;
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepo;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepo, IMapper mapper)
        {
            _teacherRepo = teacherRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TeacherDto>))]
        public IActionResult GetTeachers()
        {
            var objList = _teacherRepo.GetTeachers();
            var objDto = new List<TeacherDto>();

            foreach(var obj in objList)
            {
                objDto.Add(_mapper.Map<TeacherDto>(obj));
            }

            return Ok(objDto);
        }

        [HttpGet("{teacherId:int}",Name = nameof(GetTeacher))] //{uniqueName:type}
        [ProducesResponseType(200,Type = typeof(TeacherDto))]
        [ProducesResponseType(400)]
        public IActionResult GetTeacher(int teacherId)
        {
            var obj = _teacherRepo.GetTeacher(teacherId);

            if(obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<TeacherDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TeacherDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateTeacher(TeacherDto teacherDto)
        {
            if(teacherDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_teacherRepo.isTeacherExists(teacherDto.Name))
            {
                ModelState.AddModelError("", $"Teacher is exists!");
                return StatusCode(404, ModelState);
            }

            var teacherObj = _mapper.Map<Teacher>(teacherDto);

            if (!_teacherRepo.CreateTeacher(teacherObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {teacherObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute(nameof(GetTeacher), new { teacherId = teacherObj.Id }, teacherObj);
        }

        [HttpPatch]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateTeacher(int teacherId, TeacherDto teacherDto)
        {
            if(teacherDto == null || teacherId != teacherDto.Id)
            {
                return BadRequest(ModelState);
            }

            var teacherObj = _mapper.Map<Teacher>(teacherDto);

            if (!_teacherRepo.UpdateTeacher(teacherObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {teacherObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult DeleteTeacher(int teacherId)
        {
            if (!_teacherRepo.isTeacherExists(teacherId))
            {
                return NotFound();
            }

            var teacherObj = _teacherRepo.GetTeacher(teacherId);

            if (!_teacherRepo.DeleteTeacher(teacherObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {teacherObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
