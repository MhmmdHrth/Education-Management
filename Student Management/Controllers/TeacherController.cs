﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management.Models;
using Student_Management.Models.Dtos.TeacherDto;
using Student_Management.Repository.IRepository;
using System.Collections.Generic;

namespace Student_Management.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(Teacher))]
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

        /// <summary>
        /// Get list of all Teachers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TeacherDto>))]
        public IActionResult GetTeachers()
        {
            var objList = _teacherRepo.GetTeachers();
            var objDto = new List<TeacherDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TeacherDto>(obj));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Get list of individual Teacher
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpGet("{teacherId:int}", Name = nameof(GetTeacher))] //{uniqueName:type}
        [ProducesResponseType(200, Type = typeof(TeacherDto))]
        [ProducesResponseType(400)]
        public IActionResult GetTeacher(int teacherId)
        {
            var obj = _teacherRepo.GetTeacher(teacherId);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<TeacherDto>(obj);
            return Ok(objDto);
        }

        /// <summary>
        /// Create Teacher
        /// </summary>
        /// <param name="teacherCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TeacherDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateTeacher(TeacherCreateDto teacherCreateDto)
        {
            if (teacherCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_teacherRepo.isTeacherExists(teacherCreateDto.Name))
            {
                ModelState.AddModelError("", $"Teacher is exists!");
                return StatusCode(404, ModelState);
            }

            var teacherObj = _mapper.Map<Teacher>(teacherCreateDto);

            if (!_teacherRepo.CreateTeacher(teacherObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {teacherObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute(nameof(GetTeacher), new { teacherId = teacherObj.Id }, teacherObj);
        }

        /// <summary>
        /// Update Teacher Details
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="teacherUpdateDto"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(204, Type = typeof(TeacherDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateTeacher(int teacherId, TeacherUpdateDto teacherUpdateDto)
        {
            if (teacherUpdateDto == null || teacherId != teacherUpdateDto.Id)
            {
                return BadRequest(ModelState);
            }

            var teacherObj = _mapper.Map<Teacher>(teacherUpdateDto);

            if (!_teacherRepo.UpdateTeacher(teacherObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {teacherObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete Teacher
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
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