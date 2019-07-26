using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CounterCulture.Models;
using CounterCulture.Repositories;
using CounterCulture.Utilities;
using IdentityServer4.Test;

namespace CounterCulture.Services {

  public class TestUserStore : IUserStore<TestUser>, IUserPasswordStore<TestUser>, IUserEmailStore<TestUser>
  {
    public TestUserStore(ITestUserRepository AppUserRepository)
    {
        TestUserRepo = AppUserRepository;
    }
    ITestUserRepository TestUserRepo { get; set; }
    public Task<IdentityResult> CreateAsync(TestUser user, CancellationToken cancellationToken)
    {
      TestUserRepo.AddUser(user);
      return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> DeleteAsync(TestUser user, CancellationToken cancellationToken)
    {
      // TODO: AppUserRepo.DeleteUser(user.Id);
      throw new NotImplementedException();
    }

    public void Dispose()
    {
    }

    public Task<TestUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
      return Task.FromResult(TestUserRepo.FindByEmail(normalizedEmail));
    }

    public Task<TestUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
      return Task.FromResult(TestUserRepo.FindByID(userId));
    }

    public Task<TestUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
      return Task.FromResult(TestUserRepo.FindByUserName(normalizedUserName.ToLower()));
    }

    public Task<string> GetEmailAsync(TestUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Username);
    }

    public Task<bool> GetEmailConfirmedAsync(TestUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(false);
    }

    public Task<string> GetNormalizedEmailAsync(TestUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(TestUserRepo.FindUser(user).Username);
    }

    public Task<string> GetNormalizedUserNameAsync(TestUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Username);
    }

    public Task<string> GetPasswordHashAsync(TestUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Password);
    }

    public Task<string> GetUserIdAsync(TestUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(Guid.NewGuid().ToString());
    }

    public Task<string> GetUserNameAsync(TestUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Username);
    }

    public Task<bool> HasPasswordAsync(TestUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(!string.IsNullOrEmpty(user.Password));
    }

    public Task SetEmailAsync(TestUser user, string email, CancellationToken cancellationToken)
    {
      user.Username = email;
      return Task.CompletedTask;
    }

    public Task SetEmailConfirmedAsync(TestUser user, bool confirmed, CancellationToken cancellationToken)
    {
      //user.EmailConfirmed = confirmed;
      return Task.CompletedTask;
    }

    public Task SetNormalizedEmailAsync(TestUser user, string normalizedEmail, CancellationToken cancellationToken)
    {
      //user.NormalizedEmail = normalizedEmail;
      return Task.CompletedTask;
    }

    public Task SetNormalizedUserNameAsync(TestUser user, string normalizedName, CancellationToken cancellationToken)
    {
      //user.NormalizedUserName = normalizedName;
      return Task.CompletedTask;
    }

    public Task SetPasswordHashAsync(TestUser user, string passwordHash, CancellationToken cancellationToken)
    {
      //user.PasswordHash = passwordHash;
      return Task.CompletedTask;
    }

    public Task SetUserNameAsync(TestUser user, string userName, CancellationToken cancellationToken)
    {
      user.Username = userName;
      return Task.CompletedTask;
    }

    public Task<IdentityResult> UpdateAsync(TestUser user, CancellationToken cancellationToken)
    {
      return Task.FromResult(IdentityResult.Success);
    }
  }

}