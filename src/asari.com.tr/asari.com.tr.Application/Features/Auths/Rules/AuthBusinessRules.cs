using asari.com.tr.Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.Auths.Rules;

public class AuthBusinessRules : BaseBusinessRules
{

    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task EmailCanNotBeDuplicatedWheRegistered(string email)
    {
        var result = await _userRepository.Query().Where(x => x.Email == email).AnyAsync();
        if (result) throw new BusinessException("Mail already Exists");
    }

    public Task UserShouldBeExists(User? user)
    {
        if (user == null) throw new BusinessException("Kullanıcı mevcut değil.");
        return Task.CompletedTask;
    }

    public async Task UserPasswordShouldBeMatch(int id, string password)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id);
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException("Hatalı şifre");
    }
}