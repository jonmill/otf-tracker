using Microsoft.AspNetCore.Identity;

namespace OtfTracker.Website.Identity
{
    public class OtfUser
    {
        public string MemberId { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string Locale { get; set; }
        public string HomeStudioId { get; set; }
        public string FamilyName { get; set; }
        public bool IsMigration { get; set; }
        public string SignInJwt { get; set; }
        public string RefreshJwt { get; set; }
    }
}
