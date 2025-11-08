using Application.Models;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Domain.Interfaces;

namespace Infrastructure
{
    public class AuthenticationRepository 
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _config;

        public AuthenticationRepository(AppDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        // Retorna el token si es exitoso, null si falla
        public string? AuthenticateRepository(CredentialsDtoRequest credentials)
        {
            // Buscar usuario por nombre
            var user = _dbContext.Usuarios.FirstOrDefault(u => u.Nombre == credentials.Nombre);

            // Verificar que el usuario existe Y la contraseña es correcta
            if (user == null || !BCrypt.Net.BCrypt.Verify(credentials.Password, user.Contraseña))
            {
                return null; // Credenciales inválidas
            }

            // Usuario autenticado correctamente, generar token

            // Crear la clave segura
            var securityPassword = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

            // Algoritmo para firmar el token
            var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            // Crear los claims
            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("role", user.Rol.ToString()),
                new Claim("name", user.Nombre)
            };

            // Crear el JWT
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _config["Authentication:Issuer"],
                audience: _config["Authentication:Audience"],
                claims: claimsForToken,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signature
            );

            // Generar el token como string
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }
    }
}