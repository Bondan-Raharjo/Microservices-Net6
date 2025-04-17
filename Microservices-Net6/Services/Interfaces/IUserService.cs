using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microservices_Net6.DTOs;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;

namespace Microservices_Net6.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request);
        Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request);
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
    }

}