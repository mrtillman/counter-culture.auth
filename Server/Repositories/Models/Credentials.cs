using System;
using Microsoft.AspNetCore.Mvc;

namespace CounterCulture.Repositories.Models {

  public class Credentials {

    [FromForm]
    public string Username { get; set; }

    [FromForm]
    public string Password { get; set; }

  }

}