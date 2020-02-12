using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Presentation.Models {
    
    public class Registration {
      public string ClientName { get; set; }
      public List<string> AllowedScopes { get; set; }
      public List<string> RedirectUris { get; set; }
      public List<string> ClientSecrets { get; set; }
      
    }
}