using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _dbContext.Usuarios
                .Include(u => u.Pedidos)
                .Include(u => u.Carritos)
                .ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _dbContext.Usuarios
                .Include(u => u.Pedidos)
                .Include(u => u.Carritos)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> CreateAsync(Usuario entity)
        {
            // Hashear la contraseña antes de guardar
            entity.Contraseña = BCrypt.Net.BCrypt.HashPassword(entity.Contraseña);
            await _dbContext.Usuarios.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Usuario entity)
        {
            _dbContext.Usuarios.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Usuario?> GetUserByPhoneAsync(string telefono)
        {
            return await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Telefono == telefono);
        }

        public async Task<Usuario?> GetUserByNombreAsync(string nombre)
        {
            return await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == nombre);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return;
            }
            _dbContext.Usuarios.Remove(usuario);
            await _dbContext.SaveChangesAsync();
        }

        // AUTENTICACIÓN
        public string? AuthenticateRepository(CredentialsDtoRequest credentials)
        {
            var user = _dbContext.Usuarios.FirstOrDefault(u => u.Nombre == credentials.Nombre);

            if (user == null || !BCrypt.Net.BCrypt.Verify(credentials.Password, user.Contraseña))
            {
                return null;
            }

            // Generar token JWT
            var securityPassword = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

            var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("role", user.Rol.ToString()),
                new Claim("name", user.Nombre)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _config["Authentication:Issuer"],
                audience: _config["Authentication:Audience"],
                claims: claimsForToken,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signature
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}