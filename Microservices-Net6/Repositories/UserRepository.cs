using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;

namespace Microservices_Net6.Repositories
{
    public class UserRepository : IUserRepository
    {
        // Dummy data
        private static readonly List<User> _users = new()
        {
            new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", // admin
                Role = "Admin",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new User
            {
                Id = 2,
                Username = "user",
                Email = "user@example.com",
                PasswordHash = "04f8996da763b7a969b1028ee3007569eaf3a635486ddab211d512c85b9df8fb", // user
                Role = "User",
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            }
        };

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<User>>(_users);
        }

        public Task<User> GetByIdAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<User> GetByEmailAsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            return Task.FromResult(user);
        }

        public Task<User> CreateAsync(User user)
        {
            var maxId = _users.Count > 0 ? _users.Max(u => u.Id) : 0;
            user.Id = maxId + 1;
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User> UpdateAsync(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                if (!string.IsNullOrWhiteSpace(user.PasswordHash))
                {
                    existingUser.PasswordHash = user.PasswordHash;
                }
                existingUser.Role = user.Role;
                return Task.FromResult(existingUser);
            }
            return Task.FromResult<User>(null);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }

}