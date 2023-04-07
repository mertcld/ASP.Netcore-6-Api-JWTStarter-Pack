using JwtStarterApi.EntityFrameworkCore.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace JwtStarterApi.Permissions
{
    public class RBACAuthorizeAttribute : AuthorizeAttribute
    {
        public RBACAuthorizeAttribute(Resource resource, Operation operation) : base($"{EntityFrameworkCore.Permissions.PolicyDefinitions.RBAC}.{resource}.{(int)operation}")
        {
        }
    }
}
