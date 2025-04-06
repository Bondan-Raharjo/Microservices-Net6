using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microservices_Net6.DTOs;
using Microservices_Net6.Services.Interfaces;

namespace Microservices_Net6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDTO>> Create([FromBody] CreateDepartmentDTO departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdDepartment = await _departmentService.CreateAsync(departmentDto);
            return CreatedAtAction(nameof(GetById), new { id = createdDepartment.Id }, createdDepartment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentDTO>> Update(int id, [FromBody] UpdateDepartmentDTO departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedDepartment = await _departmentService.UpdateAsync(id, departmentDto);
            if (updatedDepartment == null)
                return NotFound();

            return Ok(updatedDepartment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _departmentService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}