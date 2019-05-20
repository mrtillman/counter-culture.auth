using System;
using Microsoft.AspNetCore.Mvc;

namespace CounterCulture.Models {

  public class UserForm {
    
    public int ID { get; set; }
    
    [FromForm]
    public string Username { get; set; }

    [FromForm]
    public string Password { get; set; }
  }

}