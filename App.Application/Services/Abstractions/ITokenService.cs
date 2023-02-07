using App.Application.Dtos;
using App.Core.Model;

namespace App.Application.Services.Abstractions
{
    public interface ITokenService
    {
        TokenReadDto GenerateToken(AppUser user);
    }
}
