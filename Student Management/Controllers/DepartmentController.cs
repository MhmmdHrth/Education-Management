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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmenRepo, IMapper mapper)
        {
            _departmentRepo = departmenRepo;
            _mapper = mapper;
        }

        [HttpGet]
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

        [HttpGet("departmentId:int", Name = "GetDepartment")]
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
        public IActionResult UpdateDepartment(int departmentId)
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
