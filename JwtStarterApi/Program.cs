using JwtStarterApi.Configurations;
using JwtStarterApi.EntityFrameworkCore;
using JwtStarterApi.EntityFrameworkCore.Models;
using JwtStarterApi.Permissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
   
    opts.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT"
    });
 
    opts.OperationFilter<SecurityRequirementsOperationFilter>(true, JwtBearerDefaults.AuthenticationScheme); 
});
builder.Services.AddDbContextPool<JWTDemoDbContext>(b => b.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
 
builder.Services.AddIdentityCore<User>(opts =>
{
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireDigit = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequiredLength = 6;
}).AddRoles<Role>().AddSignInManager().AddEntityFrameworkStores<JWTDemoDbContext>().AddDefaultTokenProviders();
 
var jwtSettings = new JwtBearerSettings();
var jwtSettingsSection = builder.Configuration.GetSection("JwtBearer");
jwtSettingsSection.Bind(jwtSettings);
builder.Services.Configure<JwtBearerSettings>(jwtSettingsSection);
builder.Services.AddAuthentication().AddJwtBearer(opts =>
{
    opts.IncludeErrorDetails = true;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
    };
});
builder.Services.AddSingleton<IAuthorizationPolicyProvider, RBACPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, OwnerOnlyAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RBACAuthorizationHandler>();
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
