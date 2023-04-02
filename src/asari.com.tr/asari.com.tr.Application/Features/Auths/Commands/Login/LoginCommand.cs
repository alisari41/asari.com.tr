using asari.com.tr.Application.Features.Auths.Rules;
using asari.com.tr.Application.Services.AuthService;
using asari.com.tr.Application.Services.UserServices;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace asari.com.tr.Application.Features.Auths.Commands.Login;

public class LoginCommand : IRequest<LoggedResponse>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedResponse>
    {
        private readonly IUserServices _userServices;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginCommandHandler(IUserServices userServices, IAuthService authService, AuthBusinessRules authBusinessRules)
        {
            _userServices = userServices;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userServices.GetByEmail(request.UserForLoginDto.Email);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user.Id, request.UserForLoginDto.Password);

            LoggedResponse loggedResponse = new();

            AccessToken createdAccessToken = await _authService.CreatedAccessToken(user);
            RefreshToken createdRefreshToken = await _authService.CreatedRefreshToken(user, request.IpAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            await _authService.DeleteOldRefreshToken(user.Id);

            loggedResponse.AccessToken = createdAccessToken;
            loggedResponse.RefreshToken = addedRefreshToken;

            return loggedResponse;
        }
    }
}