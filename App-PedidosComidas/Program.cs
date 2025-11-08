<<<<<<< HEAD
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure;
=======
﻿using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Application.Interfaces;        
using Application.Services;          
>>>>>>> abc0669 (termine de corregir todos los errores de la autenticacion)
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;  
using Microsoft.IdentityModel.Tokens;                
using System.Text;



var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
// HttpClient Factory para servicios externos
builder.Services.AddHttpClient("ExchangeRateAPI", client =>
{
    client.BaseAddress = new Uri("https://open.er-api.com/v6/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Add services to the container.
=======

>>>>>>> abc0669 (termine de corregir todos los errores de la autenticacion)
builder.Services.AddControllers();


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
<<<<<<< HEAD
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));
=======
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


var secretKey = builder.Configuration["Authentication:SecretForKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
        };
    });
>>>>>>> abc0669 (termine de corregir todos los errores de la autenticacion)

// Repositories
builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoriaRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<ICarritoService, CarritoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IPagoService, PagoService>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();  // ← Importante: agregar ANTES de UseAuthorization
app.UseAuthorization();
app.MapControllers();

app.Run();