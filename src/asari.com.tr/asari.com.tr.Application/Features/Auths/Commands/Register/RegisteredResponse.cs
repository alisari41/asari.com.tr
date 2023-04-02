using Core.Security.Entities;
using Core.Security.JWT;

namespace asari.com.tr.Application.Features.Auths.Commands.Register;

public class RegisteredResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}