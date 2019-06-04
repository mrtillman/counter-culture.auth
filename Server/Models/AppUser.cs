using System;

namespace CounterCulture.Models {
  public class AppUser
  {
      public string Id { get; set; }
      public string UserName { get; set; }
      public string Email { get; set; }
      public string NormalizeUserName { get; set; }
      public string Password { get; set; }
      public string PasswordHash { get; set; }
  }
}