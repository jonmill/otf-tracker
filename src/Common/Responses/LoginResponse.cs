using OtfTracker.Common.Models;

namespace OtfTracker.Common.Responses
{
    public class LoginResponse
    {
        public bool EmailVerified { get; set; }
        public string MemberId { get; set; }
        public string GivenName { get; set; }
        public string Locale { get; set; }
        public string HomeStudioId { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public bool IsMigration { get; set; }
        public string JwtToken { get; set; }
    }
}
