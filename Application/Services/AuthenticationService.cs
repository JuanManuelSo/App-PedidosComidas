using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _authenticationRepository;

        public AuthenticationService(IUserRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }


        public Usuario Authenticate(CredentialsDtoRequest credentials)
        {
            var user = _authenticationRepository.AuthenticateRepository(credentials);
            
            return user;
        }
    }
}
