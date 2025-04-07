using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Models.User
{
    public class UserRegisterModel
    {
        public string[]? Errors { get; set; }
        public bool IsSuccess { get; set; }
        public Guid? UserId { get; set; }
        public string Role { get; set; }
    }
}
