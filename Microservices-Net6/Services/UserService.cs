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
    
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            // Check if email already exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return null; // Email already in use
            }

            // Create new user
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateAsync(user);

            // Generate JWT token
            var token = _jwtService.GenerateToken(createdUser);

            return MapToAuthResponse(createdUser, token);
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            // Find user by email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return null; // User not found
            }

            // Verify password
            if (!VerifyPassword(request.Password, user.PasswordHash))
            {
                return null; // Invalid password
            }

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);

            return MapToAuthResponse(user, token);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            var computedHash = HashPassword(password);
            return computedHash == hash;
        }

        private AuthResponseDTO MapToAuthResponse(User user, string token)
        {
            return new AuthResponseDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token,
                Role = user.Role
            };
        }
    }
}