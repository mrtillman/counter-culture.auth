using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CounterCulture.Models {

    [Table("UserProfile")]
    public class UserProfile {
        [Key]
        public int ProfileID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
    }
}