using System;
using System.Collections.Generic;

namespace Microservices_Net6.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime JoinDate { get; set; }
        public string Position { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        public Employee()
        {
            Attendances = new List<Attendance>();
        }
    }
}