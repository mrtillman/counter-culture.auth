using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CounterCulture.Models;
using CounterCulture.Repositories;
using CounterCulture.Utilities;

namespace CounterCulture.Services {
  
  public class AppUserStore : IUserStore<AppUser>, IUserPasswordStore<AppUser>, IUserEmailStore<AppUser>
  {
    public AppUserStore(IAppUserRepository AppUserRepository)
    {
        AppUserRepo = AppUserRepository;
    }
    IAppUserRepository AppUserRepo { get; set; }
    public Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
    {
      AppUserRepo.AddUser(user);
      return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
        
    }

    public Task<AppUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
      return Task.FromResult(AppUserRepo.FindByEmail(normalizedEmail));
    }

    public Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
      return Task.FromResult(AppUserRepo.FindByID(userId));
    }

    public Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
      return Task.FromResult(AppUserRepo.FindByUserName(normalizedUserName));
    }

    public Task<string> GetEmailAsync(AppUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(AppUser user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetNormalizedEmailAsync(AppUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(AppUserRepo.FindUser(user).Email.ToLower());
    }

    public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.UserName);
    }

    public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.PasswordHash);
    }

    public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Id);
    }

    public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.UserName);
    }

    public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(!string.IsNullOrEmpty(user.Password));
    }

    public Task SetEmailAsync(AppUser user, string email, CancellationToken cancellationToken)
    {
      user.Email = email;
      return Task.CompletedTask;
    }

    public Task SetEmailConfirmedAsync(AppUser user, bool confirmed, CancellationToken cancellationToken)
    {
      // throw new NotImplementedException();
      return Task.CompletedTask;
    }

    public Task SetNormalizedEmailAsync(AppUser user, string normalizedEmail, CancellationToken cancellationToken)
    {
      user.Email = normalizedEmail;
      return Task.CompletedTask;
    }

    public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
    {
      user.NormalizeUserName = user.UserName;
      return Task.CompletedTask;
    }

    public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
    {
      user.PasswordHash = passwordHash;
      return Task.CompletedTask;
    }

    public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
    {
      user.UserName = userName;
      return Task.CompletedTask;
    }

    public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(IdentityResult.Success);
    }
  }

}