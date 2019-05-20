using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace CounterCulture.Models {

  [Table("Users")]
  public class User {
    
    public int ID { get; set; }
    
    [FromForm]
    public string Username { get; set; }

    [FromForm]
    public string Password { get; set; }
  }

}