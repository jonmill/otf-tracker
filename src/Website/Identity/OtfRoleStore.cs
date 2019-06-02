using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OtfTracker.Website.Identity
{
    public class OtfRoleStore : IRoleStore<OtfRole>
    {
        public Task<IdentityResult> CreateAsync(OtfRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(OtfRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<OtfRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<OtfRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(OtfRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(OtfRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(OtfRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(OtfRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetRoleNameAsync(OtfRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(OtfRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
