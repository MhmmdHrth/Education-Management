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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmenRepo, IMapper mapper)
        {
            _departmentRepo = departmenRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all department list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DepartmentDto>))]
        public IActionResult GetDepartments()
        {
            var objList = _departmentRepo.GetDepartments();
            var objDto = new List<DepartmentDto>();

            foreach(var obj in objList)
            {
                objDto.Add(_mapper.Map<DepartmentDto>(obj));
            }

            return Ok(objList);
        }

        /// <summary>
        /// Get individual department
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("departmentId:int", Name = "GetDepartment")]
        [ProducesResponseType(200, Type = typeof(DepartmentDto))]
        [ProducesResponseType(404)]
        public IActionResult GetDepartment(int departmentId)
        {
            var obj = _departmentRepo.GetDepartment(departmentId);

            if(obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<DepartmentDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(DepartmentDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateDepartment(DepartmentDto departmentDto)
        {
            if(departmentDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_departmentRepo.isDepartmentExists(departmentDto.Name))
            {
                ModelState.AddModelError("", "Department Exists!");
                return StatusCode(404, ModelState);
            }

            var departmentObj = _mapper.Map<Department>(departmentDto);

            if (!_departmentRepo.CreateDepartment(departmentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {departmentObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetDepartment", new { departmentId = departmentObj.Id }, departmentObj);
        }

        [HttpPatch("{departmentId:int}", Name = "UpdateDepartment")]
        [ProducesResponseType(204, Type = typeof(DepartmentDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateDepartment(int departmentId, DepartmentDto departmentDto)
        {
            if(departmentDto == null || departmentId != departmentDto.Id)
            {
                return BadRequest(ModelState);
            };

            var departmentObj = _mapper.Map<Department>(departmentDto);
            if (!_departmentRepo.UpdateDepartment(departmentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {departmentObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{departmentId:int}", Name = "DeleteDepartment")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            if (!_departmentRepo.isDepartmentExists(departmentId))
            {
                return NotFound();
            };

            var departmentObj = _departmentRepo.GetDepartment(departmentId);

            if (!_departmentRepo.DeleteDepartment(departmentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {departmentObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
