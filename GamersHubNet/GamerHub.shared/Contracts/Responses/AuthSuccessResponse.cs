namespace GamersHub.Shared.Contracts.Responses
{
    public class AuthSuccessResponse : AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
