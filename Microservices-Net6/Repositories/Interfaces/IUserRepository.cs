using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices_Net6.Models;

namespace Microservices_Net6.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }

}