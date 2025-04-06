using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices_Net6.Models;

namespace Microservices_Net6.Repositories.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAsync();
        Task<Attendance> GetByIdAsync(int id);
        Task<IEnumerable<Attendance>> GetByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Attendance>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<Attendance> GetByEmployeeAndDateAsync(int employeeId, DateTime date);
        Task<Attendance> CreateAsync(Attendance attendance);
        Task<Attendance> UpdateAsync(Attendance attendance);
        Task<bool> DeleteAsync(int id);
    }
}