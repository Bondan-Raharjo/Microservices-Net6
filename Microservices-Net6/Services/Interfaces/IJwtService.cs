using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microservices_Net6.Models;

namespace Microservices_Net6.Services
{
  
    public interface IJwtService
    {
        string GenerateToken(User user);
        bool ValidateToken(string token);
    }

}