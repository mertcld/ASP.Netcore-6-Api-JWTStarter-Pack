using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtStarterApi.EntityFrameworkCore.Models
{
    public class User : IdentityUser, IHasOwner
    {
        [NotMapped]
        public string OwnerId => Id;
    }
}
