using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthenticationService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public string? Authenticate(CredentialsDtoRequest credentials)
        {
            var user = _userRepository.GetByNombre(credentials.Nombre);

            if (user == null || !BCrypt.Net.BCrypt.Verify(credentials.Password, user.Contraseña))
                return null;

            var claims = new[]
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("role", user.Rol.ToString()),
                new Claim("name", user.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Authentication:Issuer"],
                audience: _config["Authentication:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
