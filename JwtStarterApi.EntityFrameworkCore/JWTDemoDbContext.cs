using JwtStarterApi.EntityFrameworkCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStarterApi.EntityFrameworkCore
{
    public class JWTDemoDbContext : IdentityDbContext<User, Role, string>
    {
        public JWTDemoDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
