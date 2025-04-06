using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices_Net6.DTOs;

namespace Microservices_Net6.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();
        Task<EmployeeDTO> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeDTO>> GetByDepartmentIdAsync(int departmentId);
        Task<EmployeeDTO> CreateAsync(CreateEmployeeDTO employeeDto);
        Task<EmployeeDTO> UpdateAsync(int id, UpdateEmployeeDTO employeeDto);
        Task<bool> DeleteAsync(int id);
    }
}