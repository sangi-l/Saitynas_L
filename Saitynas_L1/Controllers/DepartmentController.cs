using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynas_L1.Auth.Model;
using Saitynas_L1.Data.Dtos.Department;
using Saitynas_L1.Data.Entities;
using Saitynas_L1.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Controllers
{
    [ApiController]
    [Route("api/department")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Worker + "," + UserRoles.User)]
        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var departments = await _departmentRepository.GetAsync();
            return departments.Select(o => _mapper.Map<DepartmentDto>(o));
        }
        [HttpGet("{departmentId}")]
        [Authorize(Roles = UserRoles.Worker + "," + UserRoles.User)]
        public async Task<ActionResult<DepartmentDto>> GetAsync(int departmentId)
        {
            var department = await _departmentRepository.GetAsync(departmentId);
            if (department == null) return NotFound();

            return Ok(_mapper.Map<DepartmentDto>(department));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<DepartmentDto>> PostAsync(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);

            await _departmentRepository.InsertAsync(department);

            return Created($"/api/department/{department.Id}", _mapper.Map<DepartmentDto>(department));
        }
        [HttpPut("{departmentId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<DepartmentDto>> PutAsync(int departmentId, UpdateDepartmentDto departmentDto)
        {;
            var oldDepartment = await _departmentRepository.GetAsync(departmentId);
            if (oldDepartment == null)
                return NotFound();

            _mapper.Map(departmentDto, oldDepartment);

            await _departmentRepository.UpdateAsync(oldDepartment);

            return Ok(_mapper.Map<DepartmentDto>(oldDepartment));
        }

        [HttpDelete("{departmentId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteAsync(int departmentId)
        {
            var department = await _departmentRepository.GetAsync(departmentId);
            if (department == null)
                return NotFound();

            await _departmentRepository.DeleteAsync(department);

            return NoContent();
        }
    }
}
