using System;
using IdentityServer4.Test;

namespace CounterCulture.Repositories {

  public interface ITestUserRepository {
    bool AddUser(TestUser User);

    TestUser FindUser(TestUser User);
    TestUser FindByUserName(string UserName);

    TestUser FindByID(string UserId);
    TestUser FindByEmail(string EmailAddress);
  }

}