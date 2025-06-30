namespace SchoolProject.Data.Helpers
{
    public class JWTAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshToken refreshtoken { get; set; }
    }
    public class RefreshToken
    {
        public string UserName { get; set; }
        public string refreshTokenString { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
