namespace Payments.Domain.Responses
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimModel> Claims { get; set; }
    }

    public class ClaimModel
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
