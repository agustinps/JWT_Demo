﻿using System.ComponentModel.DataAnnotations;

namespace JWT.api.Request
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
