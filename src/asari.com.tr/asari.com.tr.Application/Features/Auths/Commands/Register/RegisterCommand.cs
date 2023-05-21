using asari.com.tr.Application.Features.Auths.Rules;
using asari.com.tr.Application.Services.AuthService;
using asari.com.tr.Application.Services.Repositories;
using Core.Application.Pipelines.Caching;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace asari.com.tr.Application.Features.Auths.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>, ICacheRemoverRequest
{
    public UserForRegisterDto UserForRegisterDto { get; set; } // Register olacak kişinin bilgileri - yani kullanıcı bilgileri
    public string IpAddress { get; set; } // RefreshToken da ip bazlı doğrulama süreçleri vardır.

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.UserCacheGroupKey };

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public RegisterCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
        {
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.EmailCanNotBeDuplicatedWheRegistered(request.UserForRegisterDto.Email);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

            #region Manuel Mapleme
            User newUser = new()
            {
                Email = request.UserForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = request.UserForRegisterDto.FirstName,
                LastName = request.UserForRegisterDto.LastName.ToUpper(),
                Status = true
            };
            #endregion

            User createdUser = await _userRepository.AddAsync(newUser); // Kullanıcıyı oluşturduk

            AccessToken createdAccessToken = await _authService.CreatedAccessToken(createdUser); // Bir tane AccessToken oluşturduk
            RefreshToken createdRefreshToken = await _authService.CreatedRefreshToken(createdUser, request.IpAddress); // Bir tane de RefreshToken oluşturduk
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken); // Veri tabanına yollamamız lazım (veri tabanına eklenen refresh token)

            RegisteredResponse registeredResponse = new()
            { // Birleştirme işlemi
                RefreshToken = addedRefreshToken,
                AccessToken = createdAccessToken
            };

            return registeredResponse;
        }
    }
}