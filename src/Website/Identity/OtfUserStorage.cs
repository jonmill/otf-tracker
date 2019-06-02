using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OtfTracker.Common;
using OtfTracker.Common.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OtfTracker.Website.Identity
{
    public class OtfUserStore : IUserLoginStore<OtfUser>
    {
        private readonly OtfApi _api;

        public OtfUserStore(OtfApi api)
        {
            _api = api;
        }

        public Task AddLoginAsync(OtfUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(OtfUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(OtfUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<OtfUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(new OtfUser()
            {
                Id = userId,
            });
        }

        public async Task<OtfUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            LoginResponse response  = await _api.LoginAsync(loginProvider, providerKey);
            OtfUser user = new OtfUser()
            {
                Email = response.Email,
                EmailConfirmed = response.EmailVerified,
                FamilyName = response.FamilyName,
                GivenName = response.GivenName,
                HomeStudioId = response.HomeStudioId,
                Id = response.MemberId,
                Locale = response.Locale,
                IsMigration = response.IsMigration,
                SignInJwt = response.JwtToken,
            };
            string serialized = JsonConvert.SerializeObject(user);
            return user;
        }

        public Task<OtfUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(OtfUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(OtfUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.GivenName);
        }

        public Task<string> GetUserIdAsync(OtfUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(OtfUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.GivenName);
        }

        public Task RemoveLoginAsync(OtfUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(OtfUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetUserNameAsync(OtfUser user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(OtfUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
