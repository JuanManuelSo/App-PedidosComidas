using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Usuario AuthenticateRepository(CredentialsDtoRequest credentials);
    }
}
