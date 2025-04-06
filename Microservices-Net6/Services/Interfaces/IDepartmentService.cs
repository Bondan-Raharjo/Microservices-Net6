using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices_Net6.DTOs;

namespace Microservices_Net6.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllAsync();
        Task<DepartmentDTO> GetByIdAsync(int id);
        Task<DepartmentDTO> CreateAsync(CreateDepartmentDTO departmentDto);
        Task<DepartmentDTO> UpdateAsync(int id, UpdateDepartmentDTO departmentDto);
        Task<bool> DeleteAsync(int id);
    }
}