using System;
using Microsoft.AspNetCore.Mvc;

namespace repositories.models {

  public class UserForm {
    
    [FromForm]
    public string Username { get; set; }

    [FromForm]
    public string Password { get; set; }
  }

}