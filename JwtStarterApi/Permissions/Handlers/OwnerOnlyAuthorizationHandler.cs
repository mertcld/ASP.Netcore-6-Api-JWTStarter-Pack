using JwtStarterApi.EntityFrameworkCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace JwtStarterApi.Permissions
{
    public class OwnerOnlyAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, IHasOwner>
    {
        private readonly UserManager<User> _userManager;

        public OwnerOnlyAuthorizationHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IHasOwner resource)
        {
            var resourceOwner = await _userManager.FindByIdAsync(resource.OwnerId);
            if (context.User.Identity?.Name == resourceOwner?.UserName)
            {
                context.Succeed(requirement);
            }
        }
    }
}
