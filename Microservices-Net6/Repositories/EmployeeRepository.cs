using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;

namespace Microservices_Net6.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // Dummy data
        private static readonly List<Employee> _employees = new()
        {
            new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@company.com",
                Phone = "555-1234",
                JoinDate = new DateTime(2020, 1, 15),
                Position = "Software Engineer",
                DepartmentId = 1
            },
            new Employee
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@company.com",
                Phone = "555-5678",
                JoinDate = new DateTime(2019, 6, 10),
                Position = "HR Manager",
                DepartmentId = 2
            },
            new Employee
            {
                Id = 3,
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bob.johnson@company.com",
                Phone = "555-9012",
                JoinDate = new DateTime(2021, 3, 22),
                Position = "Financial Analyst",
                DepartmentId = 3
            },
            new Employee
            {
                Id = 4,
                FirstName = "Alice",
                LastName = "Williams",
                Email = "alice.williams@company.com",
                Phone = "555-3456",
                JoinDate = new DateTime(2022, 2, 8),
                Position = "Marketing Specialist",
                DepartmentId = 4
            },
            new Employee
            {
                Id = 5,
                FirstName = "Charlie",
                LastName = "Brown",
                Email = "charlie.brown@company.com",
                Phone = "555-7890",
                JoinDate = new DateTime(2020, 11, 5),
                Position = "Senior Developer",
                DepartmentId = 1
            }
        };

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Employee>>(_employees);
        }

        public Task<Employee> GetByIdAsync(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(employee);
        }

        public Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            var employees = _employees.Where(e => e.DepartmentId == departmentId);
            return Task.FromResult(employees);
        }

        public Task<Employee> CreateAsync(Employee employee)
        {
            var maxId = _employees.Max(e => e.Id);
            employee.Id = maxId + 1;
            _employees.Add(employee);
            return Task.FromResult(employee);
        }

        public Task<Employee> UpdateAsync(Employee employee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Position = employee.Position;
                existingEmployee.DepartmentId = employee.DepartmentId;
                return Task.FromResult(existingEmployee);
            }
            return Task.FromResult<Employee>(null);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}