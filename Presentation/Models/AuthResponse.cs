using System;

namespace Presentation.Models {
    public class AuthResponse {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public double expires_in { get; set; }
        public DateTime? expiration_date { get; set; }
        public string refresh_token { get; set; }
    }
}