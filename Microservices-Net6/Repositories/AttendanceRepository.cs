using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;

namespace Microservices_Net6.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        // Dummy data
        private static readonly List<Attendance> _attendances = new()
        {
            new Attendance
            {
                Id = 1,
                Date = DateTime.Today.AddDays(-1),
                ClockIn = DateTime.Today.AddDays(-1).AddHours(9),
                ClockOut = DateTime.Today.AddDays(-1).AddHours(17),
                Status = "Present",
                Notes = "Regular workday",
                EmployeeId = 1
            },
            new Attendance
            {
                Id = 2,
                Date = DateTime.Today.AddDays(-1),
                ClockIn = DateTime.Today.AddDays(-1).AddHours(8).AddMinutes(45),
                ClockOut = DateTime.Today.AddDays(-1).AddHours(16).AddMinutes(30),
                Status = "Present",
                Notes = "Regular workday",
                EmployeeId = 2
            },
            new Attendance
            {
                Id = 3,
                Date = DateTime.Today.AddDays(-1),
                ClockIn = DateTime.Today.AddDays(-1).AddHours(9).AddMinutes(15),
                ClockOut = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(30),
                Status = "Late",
                Notes = "Traffic delay",
                EmployeeId = 3
            },
            new Attendance
            {
                Id = 4,
                Date = DateTime.Today,
                ClockIn = DateTime.Today.AddHours(9),
                ClockOut = null,
                Status = "Present",
                Notes = "Working",
                EmployeeId = 1
            },
            new Attendance
            {
                Id = 5,
                Date = DateTime.Today,
                ClockIn = DateTime.Today.AddHours(8).AddMinutes(50),
                ClockOut = null,
                Status = "Present",
                Notes = "Working",
                EmployeeId = 2
            }
        };

        public Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Attendance>>(_attendances);
        }

        public Task<Attendance> GetByIdAsync(int id)
        {
            var attendance = _attendances.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(attendance);
        }

        public Task<IEnumerable<Attendance>> GetByEmployeeIdAsync(int employeeId)
        {
            var attendances = _attendances.Where(a => a.EmployeeId == employeeId);
            return Task.FromResult(attendances);
        }

        public Task<IEnumerable<Attendance>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var attendances = _attendances.Where(a => a.Date >= startDate && a.Date <= endDate);
            return Task.FromResult(attendances);
        }

        public Task<Attendance> GetByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            var attendance = _attendances.FirstOrDefault(a =>
                a.EmployeeId == employeeId &&
                a.Date.Date == date.Date);

            return Task.FromResult(attendance);
        }

        public Task<Attendance> CreateAsync(Attendance attendance)
        {
            var maxId = _attendances.Max(a => a.Id);
            attendance.Id = maxId + 1;
            _attendances.Add(attendance);
            return Task.FromResult(attendance);
        }

        public Task<Attendance> UpdateAsync(Attendance attendance)
        {
            var existingAttendance = _attendances.FirstOrDefault(a => a.Id == attendance.Id);
            if (existingAttendance != null)
            {
                existingAttendance.ClockOut = attendance.ClockOut;
                existingAttendance.Status = attendance.Status;
                existingAttendance.Notes = attendance.Notes;
                return Task.FromResult(existingAttendance);
            }
            return Task.FromResult<Attendance>(null);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var attendance = _attendances.FirstOrDefault(a => a.Id == id);
            if (attendance != null)
            {
                _attendances.Remove(attendance);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}