using System;

namespace Microservices_Net6.DTOs
{
    public class AttendanceDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class CreateAttendanceDTO
    {
        public DateTime Date { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public int EmployeeId { get; set; }
    }

    public class UpdateAttendanceDTO
    {
        public DateTime? ClockOut { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }

    public class ClockInDTO
    {
        public int EmployeeId { get; set; }
        public string Notes { get; set; }
    }

    public class ClockOutDTO
    {
        public int EmployeeId { get; set; }
        public string Notes { get; set; }
    }
}