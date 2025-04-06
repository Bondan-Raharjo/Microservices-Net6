using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices_Net6.DTOs;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;
using Microservices_Net6.Services.Interfaces;

namespace Microservices_Net6.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return await MapToDtosAsync(employees);
        }

        public async Task<EmployeeDTO> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return employee != null ? await MapToDtoAsync(employee) : null;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetByDepartmentIdAsync(int departmentId)
        {
            var employees = await _employeeRepository.GetByDepartmentIdAsync(departmentId);
            return await MapToDtosAsync(employees);
        }

        public async Task<EmployeeDTO> CreateAsync(CreateEmployeeDTO employeeDto)
        {
            // Check if department exists
            var department = await _departmentRepository.GetByIdAsync(employeeDto.DepartmentId);
            if (department == null)
                return null;

            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                JoinDate = employeeDto.JoinDate,
                Position = employeeDto.Position,
                DepartmentId = employeeDto.DepartmentId
            };

            var createdEmployee = await _employeeRepository.CreateAsync(employee);
            return await MapToDtoAsync(createdEmployee);
        }

        public async Task<EmployeeDTO> UpdateAsync(int id, UpdateEmployeeDTO employeeDto)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
                return null;

            // Check if department exists
            var department = await _departmentRepository.GetByIdAsync(employeeDto.DepartmentId);
            if (department == null)
                return null;

            existingEmployee.FirstName = employeeDto.FirstName;
            existingEmployee.LastName = employeeDto.LastName;
            existingEmployee.Email = employeeDto.Email;
            existingEmployee.Phone = employeeDto.Phone;
            existingEmployee.Position = employeeDto.Position;
            existingEmployee.DepartmentId = employeeDto.DepartmentId;

            var updatedEmployee = await _employeeRepository.UpdateAsync(existingEmployee);
            return await MapToDtoAsync(updatedEmployee);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _employeeRepository.DeleteAsync(id);
        }

        private async Task<EmployeeDTO> MapToDtoAsync(Employee employee)
        {
            var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);

            return new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                JoinDate = employee.JoinDate,
                Position = employee.Position,
                DepartmentId = employee.DepartmentId,
                DepartmentName = department?.Name
            };
        }

        private async Task<IEnumerable<EmployeeDTO>> MapToDtosAsync(IEnumerable<Employee> employees)
        {
            var employeeDtos = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                employeeDtos.Add(await MapToDtoAsync(employee));
            }

            return employeeDtos;
        }
    }
}