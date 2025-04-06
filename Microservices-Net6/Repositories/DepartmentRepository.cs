using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;

namespace Microservices_Net6.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        // Dummy data
        private static readonly List<Department> _departments = new()
        {
            new Department { Id = 1, Name = "Engineering", Description = "Software Development Department" },
            new Department { Id = 2, Name = "HR", Description = "Human Resources Department" },
            new Department { Id = 3, Name = "Finance", Description = "Finance and Accounting Department" },
            new Department { Id = 4, Name = "Marketing", Description = "Marketing and Sales Department" }
        };

        public Task<IEnumerable<Department>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Department>>(_departments);
        }

        public Task<Department> GetByIdAsync(int id)
        {
            var department = _departments.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(department);
        }

        public Task<Department> CreateAsync(Department department)
        {
            var maxId = _departments.Max(d => d.Id);
            department.Id = maxId + 1;
            _departments.Add(department);
            return Task.FromResult(department);
        }

        public Task<Department> UpdateAsync(Department department)
        {
            var existingDepartment = _departments.FirstOrDefault(d => d.Id == department.Id);
            if (existingDepartment != null)
            {
                existingDepartment.Name = department.Name;
                existingDepartment.Description = department.Description;
                return Task.FromResult(existingDepartment);
            }
            return Task.FromResult<Department>(null);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var department = _departments.FirstOrDefault(d => d.Id == id);
            if (department != null)
            {
                _departments.Remove(department);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}