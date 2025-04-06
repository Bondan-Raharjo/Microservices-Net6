using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices_Net6.DTOs;

namespace Microservices_Net6.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDTO>> GetAllAsync();
        Task<AttendanceDTO> GetByIdAsync(int id);
        Task<IEnumerable<AttendanceDTO>> GetByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<AttendanceDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<AttendanceDTO> CreateAsync(CreateAttendanceDTO attendanceDto);
        Task<AttendanceDTO> UpdateAsync(int id, UpdateAttendanceDTO attendanceDto);
        Task<bool> DeleteAsync(int id);
        Task<AttendanceDTO> ClockInAsync(ClockInDTO clockInDto);
        Task<AttendanceDTO> ClockOutAsync(int id, ClockOutDTO clockOutDto);
    }
}