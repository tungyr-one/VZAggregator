using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required] 
        public string Email { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
        [Required] 
        public string City { get; set; }
    }
}
