using System;
using System.ComponentModel.DataAnnotations;

namespace Microservices_Net6.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User"; // Default role

        public DateTime CreatedAt { get; set; }
    }
}
