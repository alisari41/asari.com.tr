using Core.Security.Entities;
using Core.Security.JWT;

namespace asari.com.tr.Application.Features.Auths.Commands.RefleshToken;

public class RefreshedTokensResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}