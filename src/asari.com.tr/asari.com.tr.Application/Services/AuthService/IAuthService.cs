using Core.Security.Entities;
using Core.Security.JWT;

namespace asari.com.tr.Application.Services.AuthService;

public interface IAuthService
{
    public Task<AccessToken> CreatedAccessToken(User user);
    public Task<RefreshToken> CreatedRefreshToken(User user, string ipAddress);
    public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken); // veri tabanına refreshToken nesnesi yollamamız lazım

    //public Task SendAuthenticatorCode(User user); // Login işlemi
    public Task DeleteOldRefreshToken(int userId);
}