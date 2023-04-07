﻿using JwtStarterApi.Validates;
using System.ComponentModel.DataAnnotations;

namespace JwtStarterApi.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [AtLeastOneRequired(nameof(PhoneNumber))]
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
