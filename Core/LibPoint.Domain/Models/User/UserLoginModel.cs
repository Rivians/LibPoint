﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Models.User
{
    public class UserLoginModel
    {
        public string[]? Errors { get; set; }
        public string Role { get; set; }
        public string? Token { get; set; }
        public UserModel UserModel { get; set; }
    }
}
