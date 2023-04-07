using JwtStarterApi.EntityFrameworkCore.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace JwtStarterApi.Permissions
{
    public class RBACAuthorizationRequirement : IAuthorizationRequirement
    {
        public RBACAuthorizationRequirement(Resource resource, Operation operation)
        {
            Resource = resource;
            Operation = operation;
        }

        public Resource Resource { get; }
        public Operation Operation { get; }
    }
}
