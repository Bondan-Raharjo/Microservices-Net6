using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices_Net6.DTOs;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;
using Microservices_Net6.Services.Interfaces;

namespace Microservices_Net6.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AttendanceService(
            IAttendanceRepository attendanceRepository,
            IEmployeeRepository employeeRepository)
        {
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<AttendanceDTO>> GetAllAsync()
        {
            var attendances = await _attendanceRepository.GetAllAsync();
            return await MapToDtosAsync(attendances);
        }

        public async Task<AttendanceDTO> GetByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            return attendance != null ? await MapToDtoAsync(attendance) : null;
        }

        public async Task<IEnumerable<AttendanceDTO>> GetByEmployeeIdAsync(int employeeId)
        {
            var attendances = await _attendanceRepository.GetByEmployeeIdAsync(employeeId);
            return await MapToDtosAsync(attendances);
        }

        public async Task<IEnumerable<AttendanceDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var attendances = await _attendanceRepository.GetByDateRangeAsync(startDate, endDate);
            return await MapToDtosAsync(attendances);
        }

        public async Task<AttendanceDTO> CreateAsync(CreateAttendanceDTO attendanceDto)
        {
            // Check if employee exists
            var employee = await _employeeRepository.GetByIdAsync(attendanceDto.EmployeeId);
            if (employee == null)
                return null;

            // Check if attendance for this employee and date already exists
            var existingAttendance = await _attendanceRepository.GetByEmployeeAndDateAsync(
                attendanceDto.EmployeeId, attendanceDto.Date);

            if (existingAttendance != null)
                return null; // Already clocked in for this date

            var attendance = new Attendance
            {
                Date = attendanceDto.Date.Date, // Store date only
                ClockIn = attendanceDto.ClockIn,
                ClockOut = attendanceDto.ClockOut,
                Status = attendanceDto.Status,
                Notes = attendanceDto.Notes,
                EmployeeId = attendanceDto.EmployeeId
            };

            var createdAttendance = await _attendanceRepository.CreateAsync(attendance);
            return await MapToDtoAsync(createdAttendance);
        }

        public async Task<AttendanceDTO> UpdateAsync(int id, UpdateAttendanceDTO attendanceDto)
        {
            var existingAttendance = await _attendanceRepository.GetByIdAsync(id);
            if (existingAttendance == null)
                return null;

            existingAttendance.ClockOut = attendanceDto.ClockOut;
            existingAttendance.Status = attendanceDto.Status;
            existingAttendance.Notes = attendanceDto.Notes;

            var updatedAttendance = await _attendanceRepository.UpdateAsync(existingAttendance);
            return await MapToDtoAsync(updatedAttendance);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _attendanceRepository.DeleteAsync(id);
        }

        public async Task<AttendanceDTO> ClockInAsync(ClockInDTO clockInDto)
        {
            // Check if employee exists
            var employee = await _employeeRepository.GetByIdAsync(clockInDto.EmployeeId);
            if (employee == null)
                return null;

            // Check if attendance for today already exists
            var today = DateTime.Today;
            var existingAttendance = await _attendanceRepository.GetByEmployeeAndDateAsync(
                clockInDto.EmployeeId, today);

            if (existingAttendance != null)
                return await MapToDtoAsync(existingAttendance); // Already clocked in today

            // Create new attendance record for today
            var attendance = new Attendance
            {
                Date = today,
                ClockIn = DateTime.Now,
                ClockOut = null,
                Status = "Present",
                Notes = clockInDto.Notes,
                EmployeeId = clockInDto.EmployeeId
            };

            var createdAttendance = await _attendanceRepository.CreateAsync(attendance);
            return await MapToDtoAsync(createdAttendance);
        }

        public async Task<AttendanceDTO> ClockOutAsync(int id, ClockOutDTO clockOutDto)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
                return null;

            if (attendance.ClockOut.HasValue)
                return await MapToDtoAsync(attendance); // Already clocked out

            attendance.ClockOut = DateTime.Now;
            attendance.Notes += $" {clockOutDto.Notes}".TrimStart();

            var updatedAttendance = await _attendanceRepository.UpdateAsync(attendance);
            return await MapToDtoAsync(updatedAttendance);
        }

        private async Task<AttendanceDTO> MapToDtoAsync(Attendance attendance)
        {
            var employee = await _employeeRepository.GetByIdAsync(attendance.EmployeeId);

            return new AttendanceDTO
            {
                Id = attendance.Id,
                Date = attendance.Date,
                ClockIn = attendance.ClockIn,
                ClockOut = attendance.ClockOut,
                Status = attendance.Status,
                Notes = attendance.Notes,
                EmployeeId = attendance.EmployeeId,
                EmployeeName = employee != null ? $"{employee.FirstName} {employee.LastName}" : ""
            };
        }

        private async Task<IEnumerable<AttendanceDTO>> MapToDtosAsync(IEnumerable<Attendance> attendances)
        {
            var attendanceDtos = new List<AttendanceDTO>();

            foreach (var attendance in attendances)
            {
                attendanceDtos.Add(await MapToDtoAsync(attendance));
            }

            return attendanceDtos;
        }
    }
}