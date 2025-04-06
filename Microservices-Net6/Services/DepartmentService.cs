using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices_Net6.DTOs;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;
using Microservices_Net6.Services.Interfaces;

namespace Microservices_Net6.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return departments.Select(MapToDto);
        }

        public async Task<DepartmentDTO> GetByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            return department != null ? MapToDto(department) : null;
        }

        public async Task<DepartmentDTO> CreateAsync(CreateDepartmentDTO departmentDto)
        {
            var department = new Department
            {
                Name = departmentDto.Name,
                Description = departmentDto.Description
            };

            var createdDepartment = await _departmentRepository.CreateAsync(department);
            return MapToDto(createdDepartment);
        }

        public async Task<DepartmentDTO> UpdateAsync(int id, UpdateDepartmentDTO departmentDto)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);
            if (existingDepartment == null)
                return null;

            existingDepartment.Name = departmentDto.Name;
            existingDepartment.Description = departmentDto.Description;

            var updatedDepartment = await _departmentRepository.UpdateAsync(existingDepartment);
            return MapToDto(updatedDepartment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _departmentRepository.DeleteAsync(id);
        }

        private static DepartmentDTO MapToDto(Department department)
        {
            return new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };
        }
    }
}