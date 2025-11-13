using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Infrastructure
{
    public class UserRepository : RepositoryBase<Usuario>, IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _config;
        public UserRepository(AppDbContext dbContext, IConfiguration config) : base(dbContext)
        {
            _dbContext = dbContext;
            _config = config;
        }

        public async Task<Usuario?> GetUserByPhoneAsync(string telefono)
        {
            return await _dbContext.Usuarios

                .FirstOrDefaultAsync(u => u.Telefono == telefono);
        }

        // AUTENTICACIÓN
        //public string? AuthenticateRepository(CredentialsDto credentials)
        //{
        //    var user = _dbContext.Usuarios.FirstOrDefault(u => u.Telefono == credentials.Telefono);

        //    if (user == null)
        //    {
        //        return null; 
        //    }

        //    if (user.Contraseña != credentials.Password)
        //    {
        //        return null; // Contraseña incorrecta
        //    }

        //    // Generar token JWT
        //    var securityPassword = new SymmetricSecurityKey(
        //        Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

        //    var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

        //    var claimsForToken = new List<Claim>
        //    {
        //        new Claim("sub", user.Id.ToString()),
        //        new Claim("role", user.Rol.ToString()),
        //        new Claim("name", user.Nombre),
        //        new Claim("phone", user.Telefono)
        //    };

        //    var jwtSecurityToken = new JwtSecurityToken(
        //        issuer: _config["Authentication:Issuer"],
        //        audience: _config["Authentication:Audience"],
        //        claims: claimsForToken,
        //        notBefore: DateTime.UtcNow,
        //        expires: DateTime.UtcNow.AddHours(1),
        //        signingCredentials: signature
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        //}
    }
}
