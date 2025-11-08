using Application.Models;
using Domain.Entities;
using Domain.DTOs;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Usuario Authenticate(CredentialsDtoRequest credentials);
    }
}