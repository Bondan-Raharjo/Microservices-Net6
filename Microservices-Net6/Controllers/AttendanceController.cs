using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microservices_Net6.DTOs;
using Microservices_Net6.Services.Interfaces;

namespace Microservices_Net6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> GetAll()
        {
            var attendances = await _attendanceService.GetAllAsync();
            return Ok(attendances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDTO>> GetById(int id)
        {
            var attendance = await _attendanceService.GetByIdAsync(id);
            if (attendance == null)
                return NotFound();

            return Ok(attendance);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> GetByEmployeeId(int employeeId)
        {
            var attendances = await _attendanceService.GetByEmployeeIdAsync(employeeId);
            return Ok(attendances);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> GetByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var attendances = await _attendanceService.GetByDateRangeAsync(startDate, endDate);
            return Ok(attendances);
        }

        [HttpPost]
        public async Task<ActionResult<AttendanceDTO>> Create([FromBody] CreateAttendanceDTO attendanceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAttendance = await _attendanceService.CreateAsync(attendanceDto);
            if (createdAttendance == null)
                return BadRequest("Invalid employee ID or attendance record already exists for this date.");

            return CreatedAtAction(nameof(GetById), new { id = createdAttendance.Id }, createdAttendance);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AttendanceDTO>> Update(int id, [FromBody] UpdateAttendanceDTO attendanceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedAttendance = await _attendanceService.UpdateAsync(id, attendanceDto);
            if (updatedAttendance == null)
                return NotFound();

            return Ok(updatedAttendance);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _attendanceService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("clock-in")]
        public async Task<ActionResult<AttendanceDTO>> ClockIn([FromBody] ClockInDTO clockInDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var attendance = await _attendanceService.ClockInAsync(clockInDto);
            if (attendance == null)
                return BadRequest("Invalid employee ID.");

            return CreatedAtAction(nameof(GetById), new { id = attendance.Id }, attendance);
        }

        [HttpPut("{id}/clock-out")]
        public async Task<ActionResult<AttendanceDTO>> ClockOut(int id, [FromBody] ClockOutDTO clockOutDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var attendance = await _attendanceService.ClockOutAsync(id, clockOutDto);
            if (attendance == null)
                return NotFound();

            return Ok(attendance);
        }
    }
}