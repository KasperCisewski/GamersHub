using System;

namespace GamerHub.mobile.core.Models.Db
{
    public class UserCredentialsModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
